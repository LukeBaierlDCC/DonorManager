using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models.DonorRelated;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DonorManager.Models;
using System.Data;
using System.Drawing;
using System.Collections.Immutable;
using System.Transactions;

namespace DonorManager2024.Controllers
{
    [Authorize(Roles = "Admin, ABData")]
    public class DonorFlagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public DonorFlagsController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: DonorFlags
        public async Task<IActionResult> Index(string searchString)
        {
            //var applicationDbContext = _context.DonorFlags.Include(d => d.Donor);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _manager.Users.Include(u => u.ManagedClients).Where(u => u.Id == userId).FirstOrDefault();
            var userRoles = await _manager.GetRolesAsync(user);

            List<int> ClientIds = new List<int>();
            foreach (var Client in user.ManagedClients)
            {
                ClientIds.Add(Client.ClientId);
            }
            

            //List<int> allDonorFlags = new List<int>();

            //var allDonorFlags = userRoles.Contains("Admin") || userRoles.Contains("ABData")
            //    ? await _context.DonorFlags.ToListAsync()
            //    : await _context.DonorFlags.Include(d => d.Client).Where(d => ClientIds.Contains(d.ClientId)).ToListAsync();

            var selectedFlags = userRoles.Contains("Admin") || userRoles.Contains("ABData")
                ? await _context.DonorFlags.ToListAsync()
                : await _context.DonorFlags.Include(d => d.Client).Where(d => ClientIds.Contains(d.ClientId.Value)).ToListAsync();

            // * var flagEndpoint = await _context.DonorFlags.Include(d => d.Client).Where(d => ClientIds.Contains(d.ClientId.Value)).Include(d => d.Transactions).Where(d => ).ToListAsync();

            ViewData["CurrentFilter"] = searchString;

            if (string.IsNullOrEmpty(searchString))
                return View(selectedFlags.ToList());

            bool success = int.TryParse(searchString, out var clientId);
            if (!success)
                searchString = "";

            if (!string.IsNullOrEmpty(searchString))
                selectedFlags = selectedFlags.Where(d => d.ClientId == clientId).ToList();

            return selectedFlags.Count != 0 ?
                View(selectedFlags) :
                NotFound("No Good. Please Try Again.");

            //return View(await applicationDbContext.ToListAsync());
            //return View();
        }

        // GET: DonorFlags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _manager.Users
                .Include(c => c.ManagedClients)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (id == null || _context.DonorFlags == null || user == null)
            {
                return NotFound();
            }

            var userRoles = await _manager.GetRolesAsync(user);

            IEnumerable<Client> selectedClients;
            
            var donorFlags = await _context.DonorFlags
                //.Include(d => d.FlagId)
                .FirstOrDefaultAsync(m => m.FlagId == id);
            if (donorFlags == null)
            {
                return NotFound();
            }

            return View(donorFlags);
        }

        // GET: DonorFlags/Create
        public async Task<IActionResult> Create()
        {
            List<int> ClientIds = new List<int>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _manager.Users.Include(d => d.ManagedClients).Where(u => u.Id == userId).FirstOrDefaultAsync();
            var userRoles = await _manager.GetRolesAsync(user);

            //var allClientList = new List<Client>();
            var allClientList = await _context.Client.ToListAsync();

            bool isAdmin = false;
            if (userRoles.Contains("Admin") || userRoles.Contains("SuperAdmin") || userRoles.Contains("Client"))
            {
                isAdmin = true;
            }
            else
            {
                foreach (var Client in _context.Client.ToList()/*user.ManagedClients*/)
                {
                    allClientList.Add(Client);
                }
                Client allClientOption = new Client()
                {
                    ClientId = 0, ClientName = "All Clients"
                };
            }

            allClientList.Insert(0, new Client { ClientId = 0, ClientName = "All Clients" });

            ViewData["ClientName"] = new SelectList(isAdmin ? allClientList 
                : _context.Client.Where(c => ClientIds.Contains(c.ClientId)), "ClientId", "ClientName");                        

            return View();
        }

        // POST: DonorFlags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,/*[Bind("FlagId,DateAdded,Comment,DonorId")] */DonorFlags donorFlags)
        {
            try
            {
                var donorFlagDb = _context.DonorFlags.Include(d => d.Client).Where(d => d.FlagId == id).FirstOrDefault();
                if (donorFlags.ClientId.Value == 0)
                    donorFlags.ClientId = null;
                _context.DonorFlags.Add(donorFlags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "404 ERROR! " + "Your data failed to save. " + "Please contact your system administrator.");
            }
            return View(donorFlags);
            
        }

        // GET: DonorFlags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DonorFlags == null)
            {
                return NotFound();
            }

            var donorFlags = await _context.DonorFlags.FindAsync(id);
            if (donorFlags == null)
            {
                return NotFound();
            }
            ViewData["ClientName"] = new SelectList(_context.Client, "ClientId", "ClientName", donorFlags.FlagId);
            return View(donorFlags);
        }

        // POST: DonorFlags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("FlagId,DateAdded,Comment,DonorId")]*/ DonorFlags donorFlags)
        {
            try
            {
                _context.Update(donorFlags);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Update rejected " + "please check data " + "and try again.");
            }
            return View(donorFlags);
            //if (id != donorFlags.FlagId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(donorFlags);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!DonorFlagsExists(donorFlags.FlagId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "DonorId", donorFlags.DonorId);
            //return View(donorFlags);
        }

        // GET: DonorFlags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _manager.GetUserAsync(User);
            var userRoles = await _manager.GetRolesAsync(user);
            if (id == null || _context.DonorFlags == null)
            {
                return NotFound();
            }

            var donorFlags = await _context.DonorFlags
                //.Include(d => d.FlagId)
                .FirstOrDefaultAsync(m => m.FlagId == id);
            if (donorFlags == null)
            {
                return NotFound();
            }

            return View(donorFlags);
        }

        // POST: DonorFlags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DonorFlags == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DonorFlags'  is null.");
            }
            var donorFlags = await _context.DonorFlags.FindAsync(id);
            if (donorFlags != null)
            {
                _context.DonorFlags.Remove(donorFlags);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorFlagsExists(int id)
        {
          return (_context.DonorFlags?.Any(e => e.FlagId == id)).GetValueOrDefault();
        }
    }
}
