using Xunit;
using Moq;
using KartverketProsjekt.Controllers;
using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

            // Initialize UserManager<ApplicationUser> mock
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);

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
            _controller.ModelState.AddModelError("Title", "Title is required"); // Simulate invalid model state
            var request = new AddMapReportRequest();

            // Act
            var result = await _controller.AddForm(request);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            _mockMapReportRepository.Verify(repo => repo.AddMapReportAsync(It.IsAny<MapReportModel>()), Times.Never);
        }


        [Fact]
        public async Task AddForm_ShouldRedirectToViewReport_WhenModelStateIsValidAndUserIsFound()
        {
            // Arrange
            var request = new AddMapReportRequest
            {
                Title = "Test Report",
                Description = "Test Description",
                GeoJson = "TestGeoJson",
                MapLayerId = 1,
                County = "Test County",
                Municipality = "Test Municipality"
            };

            _mockMapReportRepository
                .Setup(repo => repo.AddMapReportAsync(It.IsAny<MapReportModel>()))
                .Returns(Task.FromResult(new MapReportModel { MapReportId = 1 })); // Return a sample MapReportModel

            // Act
            var result = await _controller.AddForm(request);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ViewReport", redirectResult.ActionName);

            _mockMapReportRepository.Verify(repo => repo.AddMapReportAsync(It.IsAny<MapReportModel>()), Times.Once);

            _mockUserManager.Verify(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

        }
    }
}