using Microsoft.AspNetCore.Mvc;

namespace KartverketProsjekt.Controllers
{
    public class ProfileManagementController : Controller
    {
        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProccessRegistration()
        {
            return null; 
        }

        [HttpPost]
        public async Task<ActionResult> Authenticate()
        {
            return null;
        }
    }
}
