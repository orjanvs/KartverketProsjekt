using Xunit;
using Moq;
using KartverketProsjekt.Controllers;
using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KartverketProsjekt.Tests
{
    public class MapReportControllerTests
    {
        private readonly MapReportController _controller;
        private readonly Mock<IMapReportRepository> _mockMapReportRepository;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;

        public MapReportControllerTests()
        {
            // Initialize the mock repository
            _mockMapReportRepository = new Mock<IMapReportRepository>();

            // Initialize UserManager<ApplicationUser> mock with only necessary dependencies
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<ApplicationUser>>(),
                new List<IUserValidator<ApplicationUser>> { Mock.Of<IUserValidator<ApplicationUser>>() },
                new List<IPasswordValidator<ApplicationUser>> { Mock.Of<IPasswordValidator<ApplicationUser>>() },
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                Mock.Of<ILogger<UserManager<ApplicationUser>>>()
                ); 
        
            // Setup user manager mock to return a specific user (you can customize this for your tests)
                    var mockUser = new ApplicationUser { Id = "testUserId" };
            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(mockUser);

            // Instantiate the controller with the mocked dependencies
            _controller = new MapReportController(_mockMapReportRepository.Object, _mockUserManager.Object);
        }


        [Fact]
        public async Task AddForm_ShouldReturnView_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Title", "Title is required");
            var request = new AddMapReportRequest();

            // Act
            var result = await _controller.AddForm(request);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _mockMapReportRepository.Verify(repo => repo.AddMapReportAsync(It.IsAny<MapReportModel>()), Times.Never);
        }

        [Fact]
        public async Task AddForm_UnauthenticatedUser_RedirectsToLoginView()
        {
            // Arrange
            _mockUserManager.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((ApplicationUser?)null);

            var request = new AddMapReportRequest
            {
                Description = "Test Description",
                Title = "Test Title",
                GeoJson = "Test GeoJson",
                MapLayerId = 1,
                County = "Test County",
                Municipality = "Test Municipality"
            };

            // Act
            var result = await _controller.AddForm(request);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Login", redirectToActionResult.ActionName);
            Assert.Equal("Account", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task AddForm_ShouldRedirectToViewReport_WhenModelStateIsValidAndUserIsFoundAndAttachmentIsAdded()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.FileName).Returns("test.jpeg");
            mockFile.Setup(f => f.Length).Returns(1024);

            var request = new AddMapReportRequest
            {
                Title = "Test Report",
                Description = "Test Description",
                GeoJson = "TestGeoJson",
                MapLayerId = 1,
                County = "Test County",
                Municipality = "Test Municipality",
                Attachments = new List<IFormFile> { mockFile.Object }
            };

            var mockMapReport = new MapReportModel { MapReportId = 1 };

            // Create a new MapReportModel to pass to the HandleAttachments method
            var newMapReport = new MapReportModel();

            _mockMapReportRepository
                .Setup(repo => repo.AddMapReportAsync(It.IsAny<MapReportModel>()))
                .ReturnsAsync(mockMapReport);

            // Act
            var result = await _controller.AddForm(request);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ViewReport", redirectResult.ActionName);
            Assert.Equal(mockMapReport.MapReportId, redirectResult.RouteValues["id"]);

            _mockMapReportRepository.Verify(repo => repo.AddMapReportAsync(It.IsAny<MapReportModel>()), Times.Once);
            _mockUserManager.Verify(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            // Verify HandleAttachments method
            var methodInfo = typeof(MapReportController).GetMethod("HandleAttachments", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(methodInfo);


            // Invoke the HandleAttachments method
            methodInfo.Invoke(_controller, new object[] { request, newMapReport });

            // Verify that the attachments were processed
            Assert.NotNull(newMapReport.Attachments);
            Assert.Single(newMapReport.Attachments);
            Assert.Equal("test.jpeg", newMapReport.Attachments.First().FilePath);
        }

        [Fact]
        public async Task DeleteMapReport_RedirectsToListForm_When_MapReportId_IsValid()
        {
            // Arrange
            _mockMapReportRepository
                .Setup(repo => repo.DeleteMapReportAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteMapReport(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ListForm", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteMapReport_ReturnsNotFoundResult_When_MapReportId_IsNotValid()
        {
            // Arrange
            _mockMapReportRepository
                .Setup(repo => repo.DeleteMapReportAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteMapReport(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Map report with ID 1 was not found or could not be deleted.", notFoundResult.Value);
        }
    }
}