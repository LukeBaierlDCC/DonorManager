using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models.MailGroup;

namespace DonorManager2024.Controllers
{
    public class NixiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NixiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nixies
        public async Task<IActionResult> Index()
        {
              return _context.Nixies != null ? 
                          View(await _context.Nixies.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Nixies'  is null.");
        }

        // GET: Nixies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nixies == null)
            {
                return NotFound();
            }

            var nixies = await _context.Nixies
                .FirstOrDefaultAsync(m => m.NixieId == id);
            if (nixies == null)
            {
                return NotFound();
            }

            return View(nixies);
        }

        // GET: Nixies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nixies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NixieId,Prefix1,First1,Middle1,Last1,Suffix1,Prefix2,First2,Middle2,Last2,Suffix2,Company,Primary,City,State,Zip")] Nixies nixies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nixies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nixies);
        }

        // GET: Nixies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nixies == null)
            {
                return NotFound();
            }

            var nixies = await _context.Nixies.FindAsync(id);
            if (nixies == null)
            {
                return NotFound();
            }
            return View(nixies);
        }

        // POST: Nixies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NixieId,Prefix1,First1,Middle1,Last1,Suffix1,Prefix2,First2,Middle2,Last2,Suffix2,Company,Primary,City,State,Zip")] Nixies nixies)
        {
            if (id != nixies.NixieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nixies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NixiesExists(nixies.NixieId))
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
            return View(nixies);
        }

        // GET: Nixies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nixies == null)
            {
                return NotFound();
            }

            var nixies = await _context.Nixies
                .FirstOrDefaultAsync(m => m.NixieId == id);
            if (nixies == null)
            {
                return NotFound();
            }

            return View(nixies);
        }

        // POST: Nixies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nixies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nixies'  is null.");
            }
            var nixies = await _context.Nixies.FindAsync(id);
            if (nixies != null)
            {
                _context.Nixies.Remove(nixies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NixiesExists(int id)
        {
          return (_context.Nixies?.Any(e => e.NixieId == id)).GetValueOrDefault();
        }
    }
}
