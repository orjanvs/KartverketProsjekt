using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Org.BouncyCastle.Asn1.Mozilla;

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

        public IActionResult AddForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        // Adds a new map report to the list of map reports
        public async Task<IActionResult> AddForm(AddMapReportRequest request)            // string geoJson, string description, int mapLayerId
        {
            var currentSubmitter = await _userManager.GetUserAsync(User);

            var newMapReport = new MapReportModel
            {
                Description = request.Description,
                Title = request.Title,
                GeoJsonString = request.GeoJson,
                MapReportStatusId = 1, // Default status for newly created map reports
                MapLayerId = request.MapLayerId,
                SubmissionDate = DateTime.Now,
                SubmitterId = currentSubmitter.Id, 
                Attachments = new List<AttachmentModel>()
            };

            HandleAttachments(request, newMapReport);

            await _mapReportRepository.AddMapReportAsync(newMapReport);

            // Redirect to view form with the id of the new map report
            return RedirectToAction("ViewReport", new { id = newMapReport.MapReportId });
            //return RedirectToAction("ListForm");
        }

        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        public async Task<IActionResult> StartHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);

            if (mapReport != null)
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
            if (mapReport != null && mapReport.CaseHandlerId == currentCaseHandler.Id)
            {
                mapReport.MapReportStatusId = 3; //Completed
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
                return RedirectToAction("ViewReport", new { id });
            }
            return Forbid();
        }

        [Authorize(Roles = "Case Handler")]
        [HttpPost]
        public async Task<IActionResult> AssignCaseHandler(int id, string newCaseHandlerId)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            var newCaseHandler = await _userManager.FindByIdAsync(newCaseHandlerId);

            if (mapReport != null && newCaseHandler != null)
            {
                mapReport.CaseHandlerId = newCaseHandler.Id;
                mapReport.CaseHandler = newCaseHandler;
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
                return RedirectToAction("ViewReport", new { id });
            }

            return BadRequest("Invalid map report or case handler.");
        }



        private void HandleAttachments(AddMapReportRequest request, MapReportModel newMapReport)
        {
            if (request.Attachments != null && request.Attachments.Count > 0)
            {
                // newMapReport.Attachments = new List<AttachmentModel>();

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

        [Authorize]
        [HttpGet]
        // Lists all map reports
        public async Task<IActionResult> ListForm(int pageNumber = 1, int pageSize = 50)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var userRole = User.IsInRole("Case Handler") ? "Case Handler" : "Submitter";

            var reports = await _mapReportRepository.GetAllMapReportsAsync(userId, userRole);

            var paginatedReports = reports
                
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
                    StatusDescription = m.MapReportStatus.StatusDescription
                })
                .ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(paginatedReports);
        }

        [Authorize]
        [HttpGet]
        // Presents view based on the id of the map report 
        public async Task<IActionResult> ViewReport(int id)
        {
            // Find map report based on id
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);

            if (mapReport != null)
            {
                var viewModel = new ViewMapReportRequest
                {
                    MapReportId = mapReport.MapReportId,
                    Description = mapReport.Description,
                    GeoJsonString = mapReport.GeoJsonString,
                    SubmissionDate = mapReport.SubmissionDate,
                    MapReportStatusId = mapReport.MapReportStatusId,
                    MapReportStatus = mapReport.MapReportStatus,
                    MapLayerId = mapReport.MapLayerId,
                    MapLayer = mapReport.MapLayer,
                    Attachments = mapReport.Attachments,
                    Submitter = mapReport.Submitter,
                    CaseHandler = mapReport.CaseHandler,

                };

                return View(viewModel);
            }

            return View(null); // Return empty view if no map report found
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


    }
}



