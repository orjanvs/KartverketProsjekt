using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace KartverketProsjekt.Controllers
{
    public class MapReportController : Controller
    {
        // private static List<MapReportModel> _mapReports = new List<MapReportModel>();
        private readonly IMapReportRepository _mapReportRepository;

        public MapReportController(IMapReportRepository mapReportRepository)
        {
            _mapReportRepository = mapReportRepository;
        }

        public IActionResult AddForm()
        {
            return View();
        }


        [HttpPost]
        // Adds a new map report to the list of map reports
        public async Task<IActionResult> AddForm(string geoJson, string description)
        {
            var newMapReport = new MapReportModel
            {
                Description = description,
                GeoJsonString = geoJson,
                MapReportStatusId = 1, // Placeholder for case status 
                MapLayerId = 1,    // Placeholder for map layer id
                SubmissionDate = DateTime.Now,
                SubmitterId = 2, // Placeholder value for test
                CaseHandlerId = 3 // Placeholder value for test

            };

            await _mapReportRepository.AddMapReportAsync(newMapReport);

            // Redirect to view form with the id of the new map report
            return RedirectToAction("ViewReport", new { id = newMapReport.MapReportId });
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
                    MapReportStatus = mapReport.MapReportStatus


                };

                return View(viewModel);
            }

            return View(null); // Return empty view if no map report found
        }
    }
}

