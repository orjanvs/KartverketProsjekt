using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace KartverketProsjekt.Controllers
{
    public class MapReportController : Controller
    {
        private static List<MapReportModel> _mapReports = new List<MapReportModel>();
        public IActionResult AddForm()
        {
            return View();
        }


        [HttpPost]
        // Adds a new map report to the list of map reports
        public IActionResult AddForm(string geoJson, string description)
        {
            var newMapReport = new MapReportModel
            {
                MapReportId = _mapReports.Count + 1,
                Description = description,
                GeoJson = geoJson,
                CaseStatus = "Ikke behandlet",
                SubmissionDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _mapReports.Add(newMapReport);
            // Redirect to view form with the id of the new map report
            return RedirectToAction("ViewForm", new { id = newMapReport.MapReportId });
        }

        [HttpGet]
        // Presents a list of all map reports
        public IActionResult ListForm()
        {
            return View(_mapReports);
        }

        [HttpGet]
        // Presents view based on the id of the map report 
        public IActionResult ViewForm(int id)
        {
            // Find map report based on id
            var mapReport = _mapReports.FirstOrDefault(m => m.MapReportId == id);
            if (mapReport != null)
            {
                var viewModel = new MapReportModel
                {
                    MapReportId = mapReport.MapReportId,
                    Description = mapReport.Description,
                    GeoJson = mapReport.GeoJson,
                    Attachment = mapReport.Attachment,
                    CaseStatus = mapReport.CaseStatus,
                    SubmissionDate = mapReport.SubmissionDate
                };
                
                return View(viewModel);
            }

            return View(null); // Return empty view if no map report found
        }
    }
}

