using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inky.Gamey.Data;
using Inky.Gamey.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Inky.Gamey.Controllers
{
    [Authorize]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SessionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var sessions = await _context.Sessions
                .Include(x => x.Game)
                .Where(s => s.CreatedBy == user.Id)
                .OrderByDescending(x => x.Time)
                .ToListAsync();

            return View(sessions);
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FirstOrDefaultAsync(m => m.Id == id);

            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Games"] = await _context.Games.ToArrayAsync();

            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameId,Time,Notes")] Session session)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                session.CreatedBy = user.Id;

                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Games"] = await _context.Games.ToArrayAsync();

            return View(session);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewData["Games"] = await _context.Games.ToArrayAsync();

            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameId,Time,Notes")] Session session)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Games"] = await _context.Games.ToArrayAsync();

            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
