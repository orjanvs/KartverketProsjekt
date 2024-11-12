using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KartverketProsjekt.Controllers
{
    // Controller for handling map report-related actions
    public class MapReportController : Controller
    {
        // private static List<MapReportModel> _mapReports = new List<MapReportModel>();

        // Repository for managing map reports and user management service
        private readonly IMapReportRepository _mapReportRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor for injecting repository and user manager dependencies
        public MapReportController(IMapReportRepository mapReportRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapReportRepository = mapReportRepository;
        }

        // GET method to display the form for adding a new map report
        public IActionResult AddForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        // Adds a new map report to the list of map reports
        public async Task<IActionResult> AddForm(AddMapReportRequest request) // string geoJson, string description, int mapLayerId
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Retrieve the current submitter from the user manager
            var currentSubmitter = await _userManager.GetUserAsync(User);

            if (currentSubmitter != null)
            {
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

            return View(); // Show error message 
        }

        [Authorize(Roles = "Case Handler")]
        [HttpPost]

        // POST method to start handling a map report, changing its status
        public async Task<IActionResult> StartHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);

            if (mapReport != null && currentCaseHandler != null)
            {
                mapReport.MapReportStatusId = 2; // Set status to "Under behandling"
                mapReport.CaseHandlerId = currentCaseHandler.Id;
                mapReport.CaseHandler = currentCaseHandler;
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
            }

            return RedirectToAction("ViewReport", new { id });
        }

        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        // POST method to finish handling a map report
        public async Task<IActionResult> FinishHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);

            if (mapReport != null && currentCaseHandler != null && mapReport.CaseHandlerId == currentCaseHandler.Id)
            {
                mapReport.MapReportStatusId = 3; // Set status to "Completed"
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
                return RedirectToAction("ViewReport", new { id });
            }
            return Forbid(); // Return forbidden if handler does not match
        }

        // Method to handle file attachments for a map report
        private void HandleAttachments(AddMapReportRequest request, MapReportModel newMapReport)
        {
            if (request.Attachments != null && request.Attachments.Count > 0)
            {
                foreach (var file in request.Attachments)
                {
                    if (file.Length > 0)
                    {
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
            }
        }

        // Method to retrieve all map reports based on user and role
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

        [Authorize]
        [HttpGet]
        // GET method to list all map reports
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

        [Authorize]
        [HttpGet]
        // GET method for displaying reports in map view
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

        [Authorize]
        [HttpGet]
        // GET method to display a specific map report by id
        public async Task<IActionResult> ViewReport(int id)
        {
            // Find map report based on id
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            var caseHandlers = await _userManager.GetUsersInRoleAsync("CASEHANDLER");

            if (mapReport != null)
            {
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

            return View(null); // Return empty view if no map report found
        }

        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        // POST method to assign a case handler to a map report
        public async Task<IActionResult> AssignCaseHandler(ViewMapReportRequest model)
        {
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(model.MapReportId);
            if (mapReport != null)
            {
                mapReport.CaseHandlerId = model.CaseHandlerId;
                var newCaseHandler = await _userManager.FindByIdAsync(model.CaseHandlerId);
                if (newCaseHandler == null)
                {
                    return NotFound("Case handler not found.");
                }
                mapReport.CaseHandler = newCaseHandler;
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
            }
            return RedirectToAction("ViewReport", new { id = model.MapReportId });
        }

        [Authorize]
        [HttpPost]
        // POST method to delete a specific map report
        public async Task<IActionResult> DeleteMapReport(int id)
        {
            var deletedReport = await _mapReportRepository.DeleteMapReportAsync(id);

            if (deletedReport != null)
            {
                return RedirectToAction("ListForm");
            }

            return RedirectToAction("ViewReport", new { id });
        }

        [Authorize]
        [HttpGet]
        // GET method to preview a specific map report
        public async Task<IActionResult> PreviewMapReport(int id)
        {
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            if (mapReport != null)
            {
                var viewModel = new ViewMapReportRequest
                {
                    MapReportId = mapReport.MapReportId,
                    Title = mapReport.Title,
                    Description = mapReport.Description,
                    SubmissionDate = mapReport.SubmissionDate
                };
                return PartialView(viewModel);
            }
            return View("MapListForm");
        }
    }
}
