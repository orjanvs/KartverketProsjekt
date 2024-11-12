using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KartverketProsjekt.Controllers
{
    /// <summary>
    /// Controller for handling home page and error-related actions.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; // Logger service for logging information and errors

        /// <summary>
        /// Constructor to inject the logger service.
        /// </summary>
        /// <param name="logger">The logger service used for logging information and errors.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GET method for the home page.
        /// </summary>
        /// <returns>The home page view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method to handle and display the error page with response caching disabled.
        /// </summary>
        /// <returns>The error view with an ErrorViewModel containing the current request ID.</returns>
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Passes an ErrorViewModel with the current request ID to the error view
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
