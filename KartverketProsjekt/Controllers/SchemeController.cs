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
			//var listAllScheme = await _context.UserTips.ToListAsync(); //retrived everything from the database
            // when active, since we cant convert the value from database to regular list, make the list in html and display the values there

            // wait until others have setup the database properly? Use list for now
            
			return View(_UsersIdeaScheme);
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
                //_context.Add(usersIdeaScheme); not adding to the database right now, but the variable add to the database
               //await _context.SaveChangesAsync(); saves to the database
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
