using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Bcpg;

namespace KartverketProsjekt.Controllers
{
    public class MapReportController : Controller
    {
        // private static List<MapReportModel> _mapReports = new List<MapReportModel>();
        private readonly IMapReportRepository _mapReportRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public MapReportController(IMapReportRepository mapReportRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _mapReportRepository = mapReportRepository;
        }

        [HttpGet]
        public IActionResult AddForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        // Adds a new map report to the list of map reports
        public async Task<IActionResult> AddForm(AddMapReportRequest request)            // string geoJson, string description, int mapLayerId
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var currentSubmitter = await _userManager.GetUserAsync(User);

            if (currentSubmitter != null)
            {
                var newMapReport = new MapReportModel
                {
                    Description = request.Description,
                    Title = request.Title,
                    GeoJsonString = request.GeoJson,
                    MapReportStatusId = 1, // Default status for newly created map reports
                    MapLayerId = request.MapLayerId,
                    SubmissionDate = DateTime.Now,
                    SubmitterId = currentSubmitter.Id,
                    Attachments = new List<AttachmentModel>(),
                    County = request.County,
                    Municipality = request.Municipality
                };


                HandleAttachments(request, newMapReport);

                await _mapReportRepository.AddMapReportAsync(newMapReport);

                // Redirect to view form with the id of the new map report
                return RedirectToAction("ViewReport", new { id = newMapReport.MapReportId });
                //return RedirectToAction("ListForm");
            }
            //Show error message
            return View();
        }

        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        public async Task<IActionResult> StartHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);

            if (mapReport != null && currentCaseHandler != null)
            {
                mapReport.MapReportStatusId = 2; // Placeholder value for status "Under behandling"
                mapReport.CaseHandlerId = currentCaseHandler.Id;
                mapReport.CaseHandler = currentCaseHandler;
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
            }

            return RedirectToAction("ViewReport", new { id });
        }

        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        public async Task<IActionResult> FinishHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            if (mapReport != null && currentCaseHandler != null && mapReport.CaseHandlerId == currentCaseHandler.Id)
            {
                mapReport.MapReportStatusId = 3; //Completed
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
                return RedirectToAction("ViewReport", new { id });
            }
            return Forbid();
        }


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
                            FilePath = fileName, // Lagre kun filnavnet
                            MapReport = newMapReport
                        };

                        // Sørg for at Attachments-listen er initialisert før vi legger til vedlegg
                        if (newMapReport.Attachments == null)
                        {
                            newMapReport.Attachments = new List<AttachmentModel>();
                        }
                        newMapReport.Attachments.Add(attachment);
                    }
                }
            }
        }

        private async Task<(string userId, string userRole, List<MapReportModel> mapReports)> GetAllMapReportsBasedOnUserAndUserRoleAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
            {
                throw new InvalidOperationException("User not found.");
            }
            var userId = user.Id;
            var userRole = User.IsInRole("Case Handler") ? "Case Handler" : "Submitter";
            var mapReports = await _mapReportRepository.GetAllMapReportsAsync(userId, userRole);
            return (userId, userRole, mapReports);
        }


        [Authorize]
        [HttpGet]
        // Lists all map reports
        public async Task<IActionResult> ListForm(int pageNumber = 1, int pageSize = 30)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var (userId, userRole, mapReports) = await GetAllMapReportsBasedOnUserAndUserRoleAsync();

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
        // Presents view based on the id of the map report 
        public async Task<IActionResult> ViewReport(int id)
        {
            // Find map report based on id
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            var caseHandlers = await _userManager.GetUsersInRoleAsync("CASEHANDLER");
            

            if (mapReport != null)
            {
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


