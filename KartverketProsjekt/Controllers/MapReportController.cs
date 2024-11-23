using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KartverketProsjekt.Controllers
{
    /// <summary>
    /// Controller for handling map report-related actions.
    /// </summary>
    public class MapReportController : Controller
    {
        // private static List<MapReportModel> _mapReports = new List<MapReportModel>();

        // Repository for managing map reports and user management service
        private readonly IMapReportRepository _mapReportRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Constructor for injecting repository and user manager dependencies.
        /// </summary>
        /// <param name="mapReportRepository">Repository to manage map reports.</param>
        /// <param name="userManager">User manager service.</param>
        public MapReportController(IMapReportRepository mapReportRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapReportRepository = mapReportRepository;
        }



        /// <summary>
        /// GET method to display the form for adding a new map report.
        /// </summary>
        /// <returns>The form view for adding a new map report.</returns>
        [HttpGet]
        public IActionResult AddForm()
        {
            return View();
        }

        /// <summary>
        /// Adds a new map report to the list of map reports.
        /// </summary>
        /// <param name="request">The request data for the new map report.</param>
        /// <returns>Redirects to the report view on success or returns to the form view on failure.</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddForm(AddMapReportRequest request) // string geoJson, string description, int mapLayerId
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Retrieve the current submitter from the user manager
            var currentSubmitter = await _userManager.GetUserAsync(User);

            if (currentSubmitter == null) return View(); // Show error message 
            // Create a new map report model from the request data
            var newMapReport = new MapReportModel
            {
                Description = request.Description,
                Title = request.Title,
                GeoJsonString = request.GeoJson,
                MapReportStatusId = 1, // Default status for new map reports
                MapLayerId = request.MapLayerId,
                SubmissionDate = DateTime.Now,
                SubmitterId = currentSubmitter.Id,
                Attachments = new List<AttachmentModel>(),
                County = request.County,
                Municipality = request.Municipality
            };

            // Handle file attachments for the map report
            HandleAttachments(request, newMapReport);

            // Add the new map report to the repository
            await _mapReportRepository.AddMapReportAsync(newMapReport);

            // Redirect to view form with the id of the new map report
            return RedirectToAction("ViewReport", new { id = newMapReport.MapReportId });
            //return RedirectToAction("ListForm");

        }

        /// <summary>
        /// POST method to start handling a map report, changing its status.
        /// </summary>
        /// <param name="id">The ID of the map report to handle.</param>
        /// <returns>Redirects to the report view after starting handling.</returns>
        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StartHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);

            if (mapReport == null || currentCaseHandler == null) return RedirectToAction("ViewReport", new { id });
            mapReport.MapReportStatusId = 2; // Set status to "Under behandling"
            mapReport.CaseHandlerId = currentCaseHandler.Id;
            mapReport.CaseHandler = currentCaseHandler;
            await _mapReportRepository.UpdateMapReportAsync(mapReport);

            return RedirectToAction("ViewReport", new { id });
        }

        /// <summary>
        /// POST method to finish handling a map report.
        /// </summary>
        /// <param name="id">The ID of the map report to finish handling.</param>
        /// <returns>Redirects to the report view on success or returns forbidden if handler does not match.</returns>
        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinishHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);

            if (mapReport == null || currentCaseHandler == null || mapReport.CaseHandlerId != currentCaseHandler.Id)
                return Forbid(); // Return forbidden if handler does not match
            mapReport.MapReportStatusId = 3; // Set status to "Completed"
            await _mapReportRepository.UpdateMapReportAsync(mapReport);
            return RedirectToAction("ViewReport", new { id });
        }

        /// <summary>
        /// Handles file attachments for a map report.
        /// </summary>
        /// <param name="request">The request containing attachments.</param>
        /// <param name="newMapReport">The map report to which attachments are added.</param>
        private void HandleAttachments(AddMapReportRequest request, MapReportModel newMapReport)
        {
            if (request.Attachments == null || request.Attachments.Count <= 0) return;
            foreach (var file in request.Attachments)
            {
                if (file.Length <= 0) continue;
                var fileName = file.FileName;
                var attachment = new AttachmentModel
                {
                    FilePath = file.FileName, // Store only the file name
                    MapReport = newMapReport
                };

                // Ensure Attachments list is initialized before adding
                if (newMapReport.Attachments == null)
                {
                    newMapReport.Attachments = new List<AttachmentModel>();
                }
                newMapReport.Attachments.Add(attachment);
            }
        }

        /// <summary>
        /// Retrieves all map reports based on user and role.
        /// </summary>
        /// <returns>A tuple with user ID, role, and list of map reports.</returns>
        private async Task<(string userId, string userRole, List<MapReportModel> mapReports)> GetAllMapReportsBasedOnUserAndUserRoleAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            //return RedirectToAction("ListForm");
            var userId = user.Id;
            var userRole = User.IsInRole("Case Handler") ? "Case Handler" : "Submitter";
            var mapReports = await _mapReportRepository.GetAllMapReportsAsync(userId, userRole);
            return (userId, userRole, mapReports);
        }

        /// <summary>
        /// GET method to list all map reports with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pageSize">The number of reports per page.</param>
        /// <returns>The view with paginated list of map reports.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListForm(int pageNumber = 1, int pageSize = 30)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var (userId, userRole, mapReports) = await GetAllMapReportsBasedOnUserAndUserRoleAsync();

            // Paginate and prepare reports for view model
            var paginatedReports = mapReports
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new ListReportsViewModel
                {
                    MapReportId = m.MapReportId,
                    SubmissionDate = m.SubmissionDate,
                    Title = m.Title,
                    Description = m.Description,
                    GeoJsonString = m.GeoJsonString,
                    MapLayerType = m.MapLayer.MapLayerType,
                    HasAttachments = m.Attachments != null && m.Attachments.Any(),
                    StatusDescription = m.MapReportStatus.StatusDescription,
                    County = m.County,
                    Municipality = m.Municipality
                })
                .ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(paginatedReports);
        }

        /// <summary>
        /// GET method for displaying reports in map view.
        /// </summary>
        /// <returns>The map list view model.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MapListForm()
        {
            var (userId, userRole, mapReports) = await GetAllMapReportsBasedOnUserAndUserRoleAsync();

            var viewModel = mapReports.Select(mapReport => new MapListViewModel
            {
                MapReportId = mapReport.MapReportId,
                GeoJsonString = mapReport.GeoJsonString,
                MapLayerId = mapReport.MapLayerId
            }).ToList();

            return View(viewModel);
        }

        /// <summary>
        /// GET method to display a specific map report by id.
        /// </summary>
        /// <param name="id">The ID of the map report.</param>
        /// <returns>The view for the specific map report.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewReport(int id)
        {
            // Find map report based on id
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            var caseHandlers = await _userManager.GetUsersInRoleAsync("CASEHANDLER");

            if (mapReport == null) return View(null); // Return empty view if no map report found
            // Create view model for map report details
            var viewModel = new ViewMapReportRequest
            {
                MapReportId = mapReport.MapReportId,
                Title = mapReport.Title,
                Description = mapReport.Description,
                GeoJsonString = mapReport.GeoJsonString,
                SubmissionDate = mapReport.SubmissionDate,
                MapReportStatusId = mapReport.MapReportStatusId,
                StatusDescription = mapReport.MapReportStatus.StatusDescription,
                MapLayerId = mapReport.MapLayerId,
                MapLayerType = mapReport.MapLayer.MapLayerType,
                County = mapReport.County,
                Municipality = mapReport.Municipality,
                Attachments = mapReport.Attachments.Select(a => new AddAttachmentRequest
                {
                    AttachmentId = a.AttachmentId,
                    MapReportId = a.MapReportId,
                    FilePath = a.FilePath
                }).ToList(),
                SubmitterId = mapReport.SubmitterId,
                SubmitterName = $"{mapReport.Submitter.FirstName} {mapReport.Submitter.LastName}",
                CaseHandlerId = mapReport.CaseHandlerId,
                CaseHandlerName = mapReport.CaseHandler != null ? $"{mapReport.CaseHandler.FirstName} {mapReport.CaseHandler.LastName}" : null,
                AvailableCaseHandlers = caseHandlers.Select(ch => new SelectListItem
                {
                    Value = ch.Id,
                    Text = $"{ch.FirstName} {ch.LastName}"
                }).ToList()
            };

            return View(viewModel);

        }

        /// <summary>
        /// POST method to assign a case handler to a map report.
        /// </summary>
        /// <param name="model">The model containing assignment details.</param>
        /// <returns>Redirects to the report view after assigning the case handler.</returns>
        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCaseHandler(ViewMapReportRequest model)
        {
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(model.MapReportId);
            if (mapReport == null)
            {
                return NotFound($"Map report with ID {model.MapReportId} not found.");
            }
            mapReport.CaseHandlerId = model.CaseHandlerId;
            var newCaseHandler = await _userManager.FindByIdAsync(model.CaseHandlerId);
            if (newCaseHandler == null)
            {
                return NotFound("Case handler not found.");
            }
            mapReport.CaseHandler = newCaseHandler;
            await _mapReportRepository.UpdateMapReportAsync(mapReport);
            return RedirectToAction("ViewReport", new { id = model.MapReportId });
        }

        /// <summary>
        /// POST method to delete a specific map report.
        /// </summary>
        /// <param name="id">The ID of the map report to delete.</param>
        /// <returns>Redirects to the list form after deletion.</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMapReport(int id)
        {
            var deletedReport = await _mapReportRepository.DeleteMapReportAsync(id);
            if (deletedReport == null)
            {
                return NotFound($"Map report with ID {id} was not found or could not be deleted.");
            }
            return RedirectToAction("ListForm"); 
        }

        /// <summary>
        /// GET method to preview a specific map report.
        /// </summary>
        /// <param name="id">The ID of the map report to preview.</param>
        /// <returns>The partial view with map report preview details.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PreviewMapReport(int id)
        {
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            if (mapReport == null) return View("MapListForm");
            var viewModel = new ViewMapReportRequest
            {
                MapReportId = mapReport.MapReportId,
                Title = mapReport.Title,
                Description = mapReport.Description,
                SubmissionDate = mapReport.SubmissionDate
            };
            return PartialView(viewModel);
        }
    }
}
