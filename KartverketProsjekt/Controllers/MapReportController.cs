﻿using Microsoft.AspNetCore.Mvc;

namespace KartverketProsjekt.Controllers
{
    public class MapReportController : Controller
    {
        public IActionResult AddForm()
        {
            return View();
        }
    }
}
