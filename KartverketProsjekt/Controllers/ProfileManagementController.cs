using Microsoft.AspNetCore.Mvc;

namespace KartverketProsjekt.Controllers
{
    public class ProfileManagementController : Controller
    {
        public IActionResult CreateAccount()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return View();
        }
    }
}
