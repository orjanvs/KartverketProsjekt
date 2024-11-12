using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KartverketProsjekt.Controllers
{
    // Controller for handling home page and error-related actions
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Logger service for logging information and errors

        // Constructor to inject the logger service
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET method for the home page
        public IActionResult Index()
        {
            return View();
        }

        // Method to handle and display error page with response caching disabled
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Passes an ErrorViewModel with the current request ID to the error view
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
