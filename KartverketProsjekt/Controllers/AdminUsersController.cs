using KartverketProsjekt.Repositories;
using Microsoft.AspNetCore.Mvc;
using KartverketProsjekt.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using KartverketProsjekt.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace KartverketProsjekt.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> List()
        {
            // Check if the current user is the sysadmin
            if (User.Identity?.Name != "sysadmin@test.com")
            {
                return Unauthorized(); // Return 401 Unauthorized if the user is not the sysadmin
            }

            var users = await GetUsersAsync();

            var usersViewModelList = new UserViewModel
            {
                Users = new List<User>()
            };

            foreach (var user in users)
            {
                if (user.Email != "sysadmin@test.com")
                {
                    usersViewModelList.Users.Add(new User
                    {
                        Id = user.Id, // Keep Id as string
                        UserName = $"{user.FirstName} {user.LastName}",
                        EmailAdress = user.Email ?? string.Empty
                    });
                }
            }

            return View(usersViewModelList);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            
            var deletedUser = await userRepository.DeleteUserById(id);
            if (deletedUser == false)
            {
                return NotFound($"User with ID {id} was not found or could not be deleted.");
            }
            return RedirectToAction("List");
        }

        private async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await userManager.Users.ToListAsync();
        }
    }
}
