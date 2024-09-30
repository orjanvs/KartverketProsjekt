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
        public IActionResult AddForm(MapReportModel mapReport)
        {
            var newMapReport = new MapReportModel
            {
                MapReportId = _mapReports.Count + 1,
                Description = mapReport.Description,
                Category = mapReport.Category,
                GeoJson = mapReport.GeoJson,
                Attachment = mapReport.Attachment,
                CaseStatus = "Open",
                SubmissionDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _mapReports.Add(newMapReport);
            return RedirectToAction("AddForm");
        }
    }
}
