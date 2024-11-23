using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KartverketProsjekt.Controllers
{
    /// <summary>
    /// Controller to handle account-related actions like registration, login, and profile management.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; // This is a service provided by ASP.NET Core Identity that allows us to interact with the user store
        private readonly SignInManager<ApplicationUser> _signInManager; // This is a service provided by ASP.NET Core Identity that allows us to sign in users

        /// <summary>
        /// Constructor to inject UserManager and SignInManager services.
        /// </summary>
        /// <param name="userManager">Service to manage user interactions.</param>
        /// <param name="signInManager">Service to manage user sign-in operations.</param>
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET method for registration page
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // GET method for login page
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // GET method to log out the user
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// POST method for registering a new user.
        /// </summary>
        /// <param name="registerViewModel">The view model containing registration details.</param>
        /// <returns>Redirects to login page on success or registration page on failure.</returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                // Creating a new ApplicationUser with details from the view model
                var identityUser = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName
                };

                // Attempt to create the user
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

        /// <summary>
        /// POST method for logging in an existing user.
        /// </summary>
        /// <param name="loginViewModel">The view model containing login details.</param>
        /// <returns>Redirects to the MapReport ListForm on success or login page on failure.</returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Attempt to sign in the user with provided credentials
            var signInResult = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
                loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                // Redirect to the MapReport ListForm on successful login
                return RedirectToAction("ListForm", "MapReport");
            }

            // Show error notification if login fails
            return View();
        }

        /// <summary>
        /// GET method for displaying the user's profile page.
        /// </summary>
        /// <returns>The profile page view with user details or redirects to login if user not found.</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfilePage()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                // Create a view model with current user's details
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
