using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models;
using DonorManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DonorManager2024.ViewModels;

namespace DonorManager2024.Controllers
{
    public class BatchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public BatchesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _manager = userManager;
        }

        // GET: Batches
        public async Task<IActionResult> Index()
        {
              return _context.Batches != null ? 
                          View(await _context.Batches.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Batches'  is null.");
        }

        // GET: Batches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Batches == null)
            {
                return NotFound();
            }

            var batches = await _context.Batches
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (batches == null)
            {
                return NotFound();
            }

            return View(batches);
        }

        // GET: Batches/Create
        public IActionResult Create()
        {
            var batchUsers = _context.Client.Where(u => u.ClientId != null).ToList();
            
            ViewBag.Client = new SelectList(batchUsers, "ClientId", "ClientName");
            BatchesViewModel batchesViewModel = new BatchesViewModel();
            //ViewData["ClientName"] = new SelectList(isAdmin ? _context.Client : _context.Client.Where(c => ClientIds.Contains(c.ClientId)), "ClientId", "ClientName");
            return View(batchesViewModel);
        }

        // POST: Batches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BatchesViewModel batchesViewModel)
        {
            try
            {
                Batches batches = new Batches();                
                batches.BatchNum = batchesViewModel.BatchNum;
                batches.PromoCat = batchesViewModel.PromoCat;
                batches.BatchCount = batchesViewModel.BatchCount;
                batches.BatchAmount = batchesViewModel.BatchAmount;
                batches.ActualCount = batchesViewModel.ActualCount;
                batches.ActualAmount = batchesViewModel.ActualAmount;
                batches.DepositError = batchesViewModel.DepositError;
                batches.TenderTypeID = batchesViewModel.TenderTypeID;
                //batches.BatchDate = batchesViewModel.BatchDate;
                
                //batches.ClientId = batchesViewModel.ClientId;
                //batches.BatchId = batchesViewModel.BatchId;

                //Client client = new Client();

                var clients = await _context.Client.Where(c => c.ClientId == batchesViewModel.SelectedClientId).FirstOrDefaultAsync();

                batches.Client = clients;
                _context.Batches.Add(batches);
                //await _manager.UpdateAsync(clients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Details", new { id=batches.BatchId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error. " + "Please check your data. " + "Then try again.");
            }
            return View(batchesViewModel);
        }

        // GET: Batches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Batches == null)
            {
                return NotFound();
            }

            var batches = await _context.Batches.FindAsync(id);
            if (batches == null)
            {
                return NotFound();
            }
            return View(batches);
        }

        // POST: Batches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BatchId,BatchNum,PromoCat,BatchCount,BatchAmount,ActualCount,ActualAmount,DepositError,TenderTypeID,BatchDate")] Batches batches)
        {
            if (id != batches.BatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(batches);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatchesExists(batches.BatchId))
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
            return View(batches);
        }

        // GET: Batches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Batches == null)
            {
                return NotFound();
            }

            var batches = await _context.Batches
                .FirstOrDefaultAsync(m => m.BatchId == id);
            if (batches == null)
            {
                return NotFound();
            }

            return View(batches);
        }

        // POST: Batches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Batches == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Batches'  is null.");
            }
            var batches = await _context.Batches.FindAsync(id);
            if (batches != null)
            {
                _context.Batches.Remove(batches);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BatchesExists(int id)
        {
          return (_context.Batches?.Any(e => e.BatchId == id)).GetValueOrDefault();
        }
    }
}
