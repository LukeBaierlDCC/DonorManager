using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models.MailGroup;
using Microsoft.AspNetCore.Authorization;

namespace DonorManager2024.Controllers
{
    public class NoMailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoMailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NoMail
        public async Task<IActionResult> Index()
        {
              return _context.NoMail != null ? 
                          View(await _context.NoMail.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NoMail'  is null.");
        }

        // GET: NoMail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NoMail == null)
            {
                return NotFound();
            }

            var noMail = await _context.NoMail
                .FirstOrDefaultAsync(m => m.NoMailId == id);
            if (noMail == null)
            {
                return NotFound();
            }

            return View(noMail);
        }

        // GET: NoMail/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName");
            return View();
        }

        // POST: NoMail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Create(NoMail noMail)
        {
            try
            {
                var client = await _context.Client.Where(c => c.ClientId == noMail.ClientId).FirstOrDefaultAsync();
                noMail.Client = client;
                _context.NoMail.Add(noMail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }
            return View(noMail);
        }

        // GET: NoMail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NoMail == null)
            {
                return NotFound();
            }

            var noMail = await _context.NoMail.FindAsync(id);
            if (noMail == null)
            {
                return NotFound();
            }
            return View(noMail);
        }

        // POST: NoMail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoMailId,ClientId,UserId,PName1,FName1,MName1,LName1,PName2,FName2,MName2,LName2,Firm,PrimAddress,SecAddress,City,State,ZIP")] NoMail noMail)
        {
            if (id != noMail.NoMailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noMail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoMailExists(noMail.NoMailId))
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
            return View(noMail);
        }

        // GET: NoMail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NoMail == null)
            {
                return NotFound();
            }

            var noMail = await _context.NoMail
                .FirstOrDefaultAsync(m => m.NoMailId == id);
            if (noMail == null)
            {
                return NotFound();
            }

            return View(noMail);
        }

        // POST: NoMail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NoMail == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NoMail'  is null.");
            }
            var noMail = await _context.NoMail.FindAsync(id);
            if (noMail != null)
            {
                _context.NoMail.Remove(noMail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoMailExists(int id)
        {
          return (_context.NoMail?.Any(e => e.NoMailId == id)).GetValueOrDefault();
        }
    }
}
