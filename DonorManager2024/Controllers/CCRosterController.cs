using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models.MailGroup;
using Microsoft.AspNetCore.Identity;
using DonorManager.Models;
using System.Security.Claims;

namespace DonorManager2024.Controllers
{
    public class CCRosterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public CCRosterController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: CCRoster
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CCRoster.Include(c => c.Client)/*.Include(c => c.Donor)*/;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CCRoster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CCRoster == null)
            {
                return NotFound();
            }

            var cCRoster = await _context.CCRoster
                .Include(c => c.Client)
                //.Include(c => c.Donor)
                .FirstOrDefaultAsync(m => m.CCRosterId == id);
            if (cCRoster == null)
            {
                return NotFound();
            }

            return View(cCRoster);
        }

        // GET: CCRoster/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserName");
            //ViewData["KeyCode"] = new SelectList(_context., "ClientId", "ClientCode");
            //ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "DonorId");
            return View();
        }

        // POST: CCRoster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CCRoster cCRoster)
        {
            try
            {
                var userIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var client = await _context.Client.Where(c => c.ClientId == cCRoster.ClientId).FirstOrDefaultAsync();
                var user = await _manager.FindByIdAsync(userIdentifier);

                cCRoster.Client = client;
                string username = user.NormalizedUserName;

                _context.CCRoster.Add(cCRoster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            //if (ModelState.IsValid)
            //{
            //    _context.Add(cCRoster);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode", cCRoster.ClientId);
            //ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "DonorId", cCRoster.DonorId);
            return View(cCRoster);
        }

        // GET: CCRoster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CCRoster == null)
            {
                return NotFound();
            }

            var cCRoster = await _context.CCRoster.FindAsync(id);
            if (cCRoster == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode", cCRoster.ClientId);
            //ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "DonorId", cCRoster.DonorId);
            return View(cCRoster);
        }

        // POST: CCRoster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CCRoster cCRoster)
        {
            if (id != cCRoster.CCRosterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cCRoster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CCRosterExists(cCRoster.CCRosterId))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode", cCRoster.ClientId);
            //ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "DonorId", cCRoster.DonorId);
            return View(cCRoster);
        }

        // GET: CCRoster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CCRoster == null)
            {
                return NotFound();
            }

            var cCRoster = await _context.CCRoster
                .Include(c => c.Client)
                //.Include(c => c.Donor)
                .FirstOrDefaultAsync(m => m.CCRosterId == id);
            if (cCRoster == null)
            {
                return NotFound();
            }

            return View(cCRoster);
        }

        // POST: CCRoster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CCRoster == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CCRoster'  is null.");
            }
            var cCRoster = await _context.CCRoster.FindAsync(id);
            if (cCRoster != null)
            {
                _context.CCRoster.Remove(cCRoster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CCRosterExists(int id)
        {
          return (_context.CCRoster?.Any(e => e.CCRosterId == id)).GetValueOrDefault();
        }
    }
}
