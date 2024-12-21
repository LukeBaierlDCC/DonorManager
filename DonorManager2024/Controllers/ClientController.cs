using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager.Models;
using DonorManager2024.Data;
using Microsoft.AspNetCore.Identity;
using DonorManager2024.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using FuzzySharp;
using DonorManager2024.Models.DonorRelated;

namespace DonorManager2024.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public ClientController(ApplicationDbContext context, UserManager<ApplicationUser> userManager/*, IHttpContextAccessor httpContextAccessor*/)
        {
            _context = context;
            _manager = userManager;
        }

        // GET: Client
        [Authorize(Roles = "Admin, ABData, Client")]
        public async Task<IActionResult> Index(string searchString, string clientName, string clientCode, string clientAddress, string clientPhone, string clientContact1, 
            string clientContact2, string clientDialog)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _manager.Users.Include(u => u.ManagedClients).Where(u => u.Id == userId).FirstOrDefault();
            var userRoles = await _manager.GetRolesAsync(user);
            //var user = await _manager.Users.Include(u => u.ManagedClients).FirstOrDefaultAsync(u => u.Id == userId);
            List<int> clientIds = new List<int>();
            foreach (var Client in user.ManagedClients)
            {
                clientIds.Add(Client.ClientId);
            }

            var selectedClients = userRoles.Contains("Admin") || userRoles.Contains("ABData")
                ? await _context.Client.ToListAsync()
                : await _context.Client.Where(c => clientIds.Contains(c.ClientId)).ToListAsync();

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterClientName"] = clientName;
            ViewData["CurrentFilterClientCode"] = clientCode;
            ViewData["CurrentFilerClientAddress"] = clientAddress;
            ViewData["CurrentFilterClientPhone"] = clientPhone;
            ViewData["CurrentFilerContact1"] = clientContact1;
            ViewData["CurrentFilterContact2"] = clientContact2;
            ViewData["CurrentFilterTransDialog"] = clientDialog;            

            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(clientName) && string.IsNullOrEmpty(clientCode) && string.IsNullOrEmpty(clientAddress) 
                && string.IsNullOrEmpty(clientPhone) && string.IsNullOrEmpty(clientContact1) && string.IsNullOrEmpty(clientContact2) && string.IsNullOrEmpty(clientDialog))
                return View(selectedClients);

            bool success = int.TryParse(searchString, out var clientId);
            if (!success)
                searchString = "";

            if (!string.IsNullOrEmpty(searchString))
                selectedClients = selectedClients.Where(c => c.ClientId == clientId).ToList();

            if (!string.IsNullOrEmpty(clientName))
                selectedClients = selectedClients.Where(c => Fuzz.Ratio(c.ClientName, clientName) > 60).ToList();

            if (!string.IsNullOrEmpty(clientCode))
                selectedClients = selectedClients.Where(c => Fuzz.Ratio(c.ClientCode, clientCode) > 60).ToList();

            if (!string.IsNullOrEmpty(clientAddress))
                selectedClients = selectedClients.Where(c => Fuzz.Ratio(c.ClientAddress, clientAddress) > 60).ToList();

            if (!string.IsNullOrEmpty(clientPhone))
                selectedClients = selectedClients.Where(c => Fuzz.Ratio(c.ClientPhone, clientPhone) > 60).ToList();

            if (!string.IsNullOrEmpty(clientContact1))
                selectedClients = selectedClients.Where(c => Fuzz.Ratio(c.Contact1, clientContact1) > 60).ToList();

            if (!string.IsNullOrEmpty(clientContact2))
                selectedClients = selectedClients.Where(c => Fuzz.Ratio(c.Contact2, clientContact2) > 60).ToList();

            if (!string.IsNullOrEmpty(clientDialog))
                selectedClients = selectedClients.Where(c => Fuzz.Ratio(c.TransDialog, clientDialog) > 60).ToList();

            //return View(clients);
            return selectedClients.Count != 0 ?
                View(selectedClients) :
                NotFound("No Clients Found");
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _manager.GetUserAsync(User);
            var isClient = await _manager.IsInRoleAsync(user, "Client");
            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            var userRoles = await _manager.GetRolesAsync(user);

            if (user.ManagedClients.Contains(client) || userRoles.Contains("Admin"))
            {
                return View(client);
            }
            else
            {
                return RedirectToAction("Home");
            }
            
        }

        public IActionResult ClientDetailsForDonor(int donorId)
        {
            var client = _context.Client.Include(c => c.Donor).Where(c => c.Donor.Any(d => d.DonorId == donorId)).FirstOrDefault();

            if (client == null)
            {
                return NotFound();
            }

            return View("Details", client);
        }

        // GET: Client/Create
        [Authorize(Roles = "Admin, ABData")]
        public IActionResult Create()
        {
            var users = _manager.Users.Where(u => u.Id != null).ToList();            
            List<ApplicationUser> usersList = new List<ApplicationUser>();
            usersList.Add(new ApplicationUser());
            foreach (var user in users)
            {
                usersList.Add(user);
            }
            ViewBag.Users = new SelectList(usersList, "Id", "UserName");
            CreateClientViewModel createClientViewModel = new CreateClientViewModel();

            return View(createClientViewModel);
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel createClientViewModel)
        {
            try
            {
                Client client = new Client();
                client.ClientCode = createClientViewModel.ClientCode;
                client.ClientName = createClientViewModel.ClientName;
                client.ClientAddress = createClientViewModel.ClientAddress;
                client.ClientPhone = createClientViewModel.ClientPhone;
                client.Contact1 = createClientViewModel.Contact1;
                client.Contact2 = createClientViewModel.Contact2;
                client.TransDialog = createClientViewModel.TransDialog;

                var user = await _manager.FindByIdAsync(createClientViewModel.SelectedUserId);
                user.ManagedClients.Add(client);

                _context.Client.Add(client);                
                await _manager.UpdateAsync(user);                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            
            return View(createClientViewModel);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var client = await _context.Client.FindAsync(id);
            var user = await _manager.GetUserAsync(User);
            var userRoles = await _manager.GetRolesAsync(user);

            if (user.ManagedClients.Contains(client) || userRoles.Contains("Admin"))
            {
                return View(client);
            }
            else
            {
                return RedirectToAction("Home");
            }

            //return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientCode,ClientName,ClientAddress,ClientPhone,Contact1,Contact2,TransDialog")] Client client)
        {
            try
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }
            return View();
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.ClientId == id);
            var user = await _manager.GetUserAsync(User);
            var userRoles = await _manager.GetRolesAsync(user);

            if (user.ManagedClients.Contains(client) || userRoles.Contains("Admin"))
            {
                return View(client);
            }
            else
            {
                return RedirectToAction("Home");
            }

            //if (id == null || _context.Client == null)
            //{
            //    return NotFound();
            //}
                        
            //if (client == null)
            //{
            //    return NotFound();
            //}

            //return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);

            if (_context.Client == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Client'  is null.");
            }
            
            if (client != null)
            {
                _context.Client.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Client?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
