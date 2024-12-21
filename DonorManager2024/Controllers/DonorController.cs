using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager.Models;
using DonorManager2024.Models;
using DonorManager2024.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using DonorManager2024.Utilities;
using static FuzzySearch.AspNetCore.DamerauLevenshteinDistances.DamerauLevenshteinDistance;
using FuzzySharp;
using DonorManager2024.Models.UsersRelated;
using DonorManager2024.Models.DonorRelated;

namespace DonorManager2024.Controllers
{    
    public class DonorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DonorController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _manager = userManager;
            _signInManager = signInManager;
        }

        // GET: Donor
        [Authorize(Roles = "Admin, ABData, Client")]
        public async Task<IActionResult> Index(string searchString, string donorName, string donorSurname,
            string donorAddress, string donorFirm, string donorPhone, string donorEmployer, string donorEmail,
            string donorWork, string donorCity, string donorZip, string donorJoin, string donorClient)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _manager.Users.Include(c => c.ManagedClients).Where(u => u.Id == userId).FirstOrDefault();
            var userRoles = await _manager.GetRolesAsync(user);

            List<int> ClientIds = new List<int>();
            foreach (var Client in user.ManagedClients)
            {
                ClientIds.Add(Client.ClientId);
            }

            var selectedDonors =  userRoles.Contains("Admin") || userRoles.Contains("ABData")
                ? await _context.Donor.ToListAsync()
                : await _context.Donor.Include(d => d.Client).Where(d => ClientIds.Contains(d.ClientId)).ToListAsync();

            //var clients = string.IsNullOrEmpty(searchString)
            //    ? await _context.Client.Include(c => c.Donor).ToListAsync()
            //    : await _context.Client.Where(c => c.Donor.Any(d => d.DonorId.ToString().Contains(searchString))).ToListAsync();

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterName"] = donorName;
            ViewData["CurrentFilterSurname"] = donorSurname;
            ViewData["CurrentFilterAddress"] = donorAddress;
            ViewData["CurrentFilterFirm"] = donorFirm;
            ViewData["CurrentFilterPhone"] = donorPhone;
            ViewData["CurrentFilterEmployer"] = donorEmployer;
            ViewData["CurrentFilterEmail"] = donorEmail;
            ViewData["CurrentFilterWork"] = donorWork;
            ViewData["CurrentFilterCity"] = donorCity;
            ViewData["CurrentFilterZip"] = donorZip;
            ViewData["CurrentFilterJoin"] = donorJoin;
            ViewData["CurrentFilterClient"] = donorClient;

            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(donorName)
                && string.IsNullOrEmpty(donorSurname) && string.IsNullOrEmpty(donorAddress)
                && string.IsNullOrEmpty(donorFirm) && string.IsNullOrEmpty(donorPhone)
                && string.IsNullOrEmpty(donorEmployer) && string.IsNullOrEmpty(donorEmail)
                && string.IsNullOrEmpty(donorWork) && string.IsNullOrEmpty(donorCity)
                && string.IsNullOrEmpty(donorZip) && string.IsNullOrEmpty(donorJoin)
                && string.IsNullOrEmpty(donorClient))
                return View(selectedDonors);

            bool success = int.TryParse(searchString, out var donorId);
            if (!success)
                searchString = "";

            if (!string.IsNullOrEmpty(searchString))
                selectedDonors = selectedDonors.Where(d => d.DonorId == donorId).ToList();

            if (!string.IsNullOrEmpty(donorName))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.FirstName, donorName) > 50).ToList();

            if (!string.IsNullOrEmpty(donorSurname))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.LastName, donorSurname) > 30).ToList();

            if (!string.IsNullOrEmpty(donorFirm))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.Firm, donorFirm) > 75).ToList();

            if (!string.IsNullOrEmpty(donorAddress))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.PrimaryAddress, donorAddress) > 50).ToList();

            if (!string.IsNullOrEmpty(donorPhone))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.Phone, donorPhone) > 70).ToList();

            if (!string.IsNullOrEmpty(donorEmployer))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.Employer, donorEmployer) > 65).ToList();

            if (!string.IsNullOrEmpty(donorEmail))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.Email, donorEmail) > 45).ToList();

            if (!string.IsNullOrEmpty(donorWork))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.WorkPhone, donorWork) > 30).ToList();

            if (!string.IsNullOrEmpty(donorCity))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.City, donorCity) > 30).ToList();

            if (!string.IsNullOrEmpty(donorZip))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.Zip, donorZip) > 80).ToList();

            if (!string.IsNullOrEmpty(donorJoin))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.JoinDate.ToString(), donorJoin) > 60).ToList();

            if (!string.IsNullOrEmpty(donorClient))
                selectedDonors = selectedDonors.Where(d => Fuzz.Ratio(d.ClientId.ToString(), donorClient) > 60).ToList();

            return selectedDonors.Count != 0 ?
                View(selectedDonors) :
                NotFound("No Donors Found");
        }

        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _manager.Users
                .Include(c => c.ManagedClients)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _manager.GetRolesAsync(user);

            if (id == null)
            {
                return NotFound();
            }

            IEnumerable<Donor> selectedDonors;

            if (userRoles.Contains("Admin") || userRoles.Contains("ABData"))
            {
                selectedDonors = await _context.Donor.Include(d => d.Client).Where(d => d.DonorId == id).ToListAsync();
            }
            else
            {
                var clientIds = user.ManagedClients.Select(c => c.ClientId).ToList();
                selectedDonors = await _context.Donor.Include(d => d.Client)
                    .Where(d => clientIds.Contains(d.ClientId) && d.DonorId == id)
                    .ToListAsync();
                                
                if (!selectedDonors.Any())
                {
                    selectedDonors = await _context.Donor.Include(d => d.Client)
                        .Where(d => d.ClientId == user.ManagedClients.FirstOrDefault().ClientId && d.DonorId == id)
                        .ToListAsync();
                }
            }

            if (!selectedDonors.Any())
            {
                return NotFound("Donor not found");
            }

            return View(selectedDonors);

        }

        // GET: Donor/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            List<int> ClientIds = new List<int>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _manager.Users.Include(c => c.ManagedClients).Where(u => u.Id == userId).FirstOrDefault();
            var userRoles = await _manager.GetRolesAsync(user);
            //var donorFlagCheckBoxes = _context.DonorFlagCheckBoxes.ToList();
            bool isAdmin = false;
            if (userRoles.Contains("Admin") || userRoles.Contains("SuperAdmin"))
            {
                isAdmin = true;
            }
            else
            {
                foreach (var Client in user.ManagedClients)
                {
                    ClientIds.Add(Client.ClientId);
                }
            }

            ViewData["ClientName"] = new SelectList(isAdmin ? _context.Client : _context.Client.Where(c => ClientIds.Contains(c.ClientId)), "ClientId", "ClientName");
            
            return View();
        }

        // POST: Donor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, Donor donor)
        {            
            try
            {
                var donorDb = _context.Donor.Include(d => d.Client).Where(d => d.DonorId == id).FirstOrDefault();

                _context.Donor.Add(donor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            //var donorFlagCheckBoxes = _context.DonorFlagCheckBoxes.ToList();
            //ViewData["DonorFlagCheckBoxes"] = new SelectList(donorFlagCheckBoxes, "DonorFlagCheckId");

            return View(donor);
        }

        // GET: Donor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _manager.Users
                                    .Include(u => u.ManagedClients)
                                    .FirstOrDefaultAsync(u => u.Id == userId);            

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _manager.GetRolesAsync(user);

            if (id == null)
            {
                return NotFound();
            }

            var donor = await _context.Donor.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }

            if (userRoles.Contains("Admin") || userRoles.Contains("ABData"))
            {
                ViewData["ClientName"] = new SelectList(_context.Client, "ClientId", "ClientName", donor.ClientId);
                ViewData["FlagName"] = new SelectList(_context.DonorFlags, "FlagId", "FlagName", donor.FlagId);
                return View(donor);
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        // POST: Donor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Donor donor)
        {
            try
            {
                var donorDb = _context.Donor.Include(d => d.Client).Where(d => d.DonorId == id).FirstOrDefault();
                donorDb.DeepCopy(donor);
                _context.Update(donorDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }
            return View();
        }

        // GET: Donor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _manager.GetUserAsync(User);
            var userRoles = await _manager.GetRolesAsync(user);
            if (id == null || _context.Donor == null)
            {
                return NotFound();
            }

            var donor = await _context.Donor
                .Include(d => d.Client)
                .FirstOrDefaultAsync(m => m.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }

            if (userRoles.Contains("Admin") || userRoles.Contains("ABData"))
            {
                return View(donor);
            }
            else
            {
                return RedirectToAction("Home");
            }

            //return View(donor);
        }

        // POST: Donor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Donor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Donor'  is null.");
            }
            var donor = await _context.Donor.FindAsync(id);
            if (donor != null)
            {
                _context.Donor.Remove(donor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorExists(int id)
        {
          return (_context.Donor?.Any(e => e.DonorId == id)).GetValueOrDefault();
        }
    }
}
