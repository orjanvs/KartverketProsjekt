using KartverketProsjekt.Data;
using KartverketProsjekt.Models.IdeaSchemeModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KartverketProsjekt.Controllers
{
    public class SchemeController : Controller
    {
        private static List<UsersIdeaScheme> _UsersIdeaScheme = new List<UsersIdeaScheme>();
        private static List<UsersIdeaScheme> _DatabaseScheme = new List<UsersIdeaScheme>();
        private readonly KartverketDbContext _context;

        public SchemeController(KartverketDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> BehandleForslag()
        {
			var listAllScheme = await _context.UserTips.ToListAsync(); //retrived everything from the database
            //since we cant convert to regular list, make the list in html and display the values there
            
			return View(listAllScheme);
		}

        [HttpGet]
        public IActionResult BrukerForslag()
		{
			return View();
		}


		[HttpPost]
        public async Task<IActionResult> BrukerForslag(UsersIdeaScheme usersIdeaScheme)
        {
            if(ModelState.IsValid)
            {
                _UsersIdeaScheme.Add(usersIdeaScheme);
                _context.Add(usersIdeaScheme);
                _context.SaveChangesAsync();
                return RedirectToAction("ForslagMotatt", usersIdeaScheme);
            }

            return View(usersIdeaScheme);
        }

       
        public async Task<ActionResult> IdeForslag()
        {
            return View();
        }

        public IActionResult ForslagMotatt()
        {
            return View(_UsersIdeaScheme);
        }

    }
}
