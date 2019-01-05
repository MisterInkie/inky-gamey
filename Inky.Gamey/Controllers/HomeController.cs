using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inky.Gamey.Models;
using Inky.Gamey.Data;
using Microsoft.AspNetCore.Identity;

namespace Inky.Gamey.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // ApplicationDbContext is our 'connection' to the database
            _context = context;

            // We use UserManager to get info on the current user
            _userManager = userManager;
        }


        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            // If we're logged in, grab any recent game sessions for this user
            if (User.Identity.IsAuthenticated)
            {
                // Get current user
                var user = await _userManager.GetUserAsync(HttpContext.User);

                // Get all sessions for games created by this user
                var sessions = await _context.Sessions
                    .Include(s => s.Game)
                    .Where(s => s.CreatedBy == user.Id)
                    .OrderByDescending(s => s.Time)
                    .Take(3)
                    .ToListAsync();

                ViewData["Sessions"] = sessions;
            }

            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
