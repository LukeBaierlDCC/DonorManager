using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models;
using Microsoft.AspNetCore.Identity;
using DonorManager.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FuzzySharp;
using System.Threading.Channels;

namespace DonorManager2024.Controllers
{
    public class PromotionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PromotionsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Promotions
        [Authorize(Roles = "Admin, ABData")]
        public async Task<IActionResult> Index(string searchString, string promotionCode, string description, string fundType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.Users.Include(u => u.ManagedClients).Where(u => u.Id == userId).FirstOrDefault();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<int> allPromotionIds = new List<int>();

            var selectedPromos = userRoles.Contains("Admin") || userRoles.Contains("ABData")
                ? await _context.Promotions.ToListAsync()
                : await _context.Promotions.Where(c => allPromotionIds.Contains(c.PromotionId)).ToListAsync();

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterPromotionCode"] = promotionCode;
            ViewData["CurrentFilterPromotionDesc"] = description;
            ViewData["CurrentFilterFundType"] = fundType;

            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(promotionCode)
                && string.IsNullOrEmpty(description) && string.IsNullOrEmpty(fundType))
                return View(selectedPromos.ToList());

            bool success = int.TryParse(searchString, out var promoId);
            if (!success)
                searchString = "";

            if (!string.IsNullOrEmpty(searchString))
                selectedPromos = selectedPromos.Where(c => c.PromotionId == promoId).ToList();

            if (!string.IsNullOrEmpty(promotionCode))
                selectedPromos = selectedPromos.Where(d => Fuzz.Ratio(d.PromotionCode, promotionCode) > 70).ToList();

            if (!string.IsNullOrEmpty(description))
                selectedPromos = selectedPromos.Where(d => Fuzz.Ratio(d.Description, description) > 70).ToList();

            if (!string.IsNullOrEmpty(fundType))
                selectedPromos = selectedPromos.Where(d => Fuzz.Ratio(d.FundType, fundType) > 70).ToList();

            return selectedPromos.Count != 0 ?
                 View(selectedPromos) :
                 NotFound("No Channels Found, Bruh");

            //return _context.Promotions != null ? 
            //              View(await _context.Promotions.ToListAsync()) :
            //              Problem("Entity set 'ApplicationDbContext.Promotions'  is null.");
        }

        // GET: Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }

            var promotions = await _context.Promotions
                .FirstOrDefaultAsync(m => m.PromotionId == id);
            if (promotions == null)
            {
                return NotFound();
            }

            return View(promotions);
        }

        // GET: Promotions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromotionId,PromotionCode,Description,FundType")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promotions);
        }

        // GET: Promotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }

            var promotions = await _context.Promotions.FindAsync(id);
            if (promotions == null)
            {
                return NotFound();
            }
            return View(promotions);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromotionId,PromotionCode,Description,FundType")] Promotions promotions)
        {
            if (id != promotions.PromotionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionsExists(promotions.PromotionId))
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
            return View(promotions);
        }

        // GET: Promotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }

            var promotions = await _context.Promotions
                .FirstOrDefaultAsync(m => m.PromotionId == id);
            if (promotions == null)
            {
                return NotFound();
            }

            return View(promotions);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Promotions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Promotions'  is null.");
            }
            var promotions = await _context.Promotions.FindAsync(id);
            if (promotions != null)
            {
                _context.Promotions.Remove(promotions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromotionsExists(int id)
        {
          return (_context.Promotions?.Any(e => e.PromotionId == id)).GetValueOrDefault();
        }
    }
}
