using KartverketProsjekt.Models;
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
            return RedirectToAction("ListForm");
        }

        [HttpGet]
        public IActionResult ListForm()
        {
            return View(_mapReports);
        }
    }
}
