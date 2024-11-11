using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KartverketProsjekt.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; // This is a service provided by ASP.NET Core Identity that allows us to interact with the user store
        private readonly SignInManager<ApplicationUser> _signInManager; // This is a service provided by ASP.NET Core Identity that allows us to sign in users

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                var identityUser = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName


                };

                var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);

                if (identityResult.Succeeded)
                {
                    // assign this user the "User" role
                    var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "Submitter");

                    if (roleIdentityResult.Succeeded)
                    {
                        //show success notification
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        // Show error notification
                        return RedirectToAction("Register");
                    }

                }
            }
            // Show error notification
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                return RedirectToAction("ListForm", "MapReport");
            }

            // Show error notification
            return View();
        } 



            [HttpGet]
            public async Task<IActionResult> ProfilePage()
            {
                var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var profilePageViewModel = new ProfilePageViewModel
                {
                    Email = currentUser.Email,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName
                };
                return View(profilePageViewModel); 
            }
            // Show error notification
            return RedirectToAction("Login");
        }
    }
}

