using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

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

        [HttpPost]
        // Adds a new map report to the list of map reports
        public async Task<IActionResult> AddForm(AddMapReportRequest request)            // string geoJson, string description, int mapLayerId
        {
            var currentSubmitter = await _userManager.GetUserAsync(User);

            var newMapReport = new MapReportModel
            {
                Description = request.Description,
                GeoJsonString = request.GeoJson,
                MapReportStatusId = 1, // Placeholder for case status 
                MapLayerId = request.MapLayerId,
                SubmissionDate = DateTime.Now,
                SubmitterId = currentSubmitter.Id, // Placeholder value for test
                Attachments = new List<AttachmentModel>()
            };

            HandleAttachments(request, newMapReport);

            await _mapReportRepository.AddMapReportAsync(newMapReport);

            // Redirect to view form with the id of the new map report
            return RedirectToAction("ViewReport", new { id = newMapReport.MapReportId });
            //return RedirectToAction("ListForm");
        }

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

        [HttpPost]
        public async Task<IActionResult> FinishHandlingMapReport(int id)
        {
            var currentCaseHandler = await _userManager.GetUserAsync(User);
            var mapReport = await _mapReportRepository.GetMapReportByIdAsync(id);
            if (mapReport != null)
            {
                mapReport.MapReportStatusId = 3; //Completed
                await _mapReportRepository.UpdateMapReportAsync(mapReport);
            }
            return RedirectToAction("ViewReport", new { id });
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

        [HttpGet]
        // Presents a list of all map reports
        public async Task<IActionResult> ListForm()
        {
            var mapReports = await _mapReportRepository.GetAllMapReportsAsync();
            return View(mapReports);
        }

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
                    Attachments = mapReport.Attachments,
                    Submitter = mapReport.Submitter,
                    CaseHandler = mapReport.CaseHandler
                };

                return View(viewModel);
            }

            return View(null); // Return empty view if no map report found
        }
    }
    }



