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
using Microsoft.PowerBI.Api.Models;
using Microsoft.AspNetCore.Authorization;
using FuzzySharp;
using DonorManager2024.Models.DonorRelated;
using DonorManager2024.ViewModels;
using System.Data.Common;
using System.Drawing;

namespace DonorManager2024.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public TransactionsController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: Transactions
        [Authorize(Roles = "Admin, ABData")]
        public async Task<IActionResult> Index(string searchString, string donorName, string campaignName)
        {
            //var applicationDbContext = _context.Transactions.Include(t => t.Campaign).Include(t => t.Donor);            

            var selectedTransactions = await _context.Transactions.Include(t => t.Donor).Include(t => t.Campaign).ToListAsync();
            
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterDonor"] = donorName;
            ViewData["CurrentFilterCampaign"] = campaignName;

            //if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(donorName) && string.IsNullOrEmpty(campaignName)
            //    return View(selectedTransactions));

            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(donorName) && string.IsNullOrEmpty(campaignName))
                return View(selectedTransactions);

            bool success = int.TryParse(searchString, out var transId);
            if (!success)
                searchString = "";

            //if (!string.IsNullOrEmpty(searchString))
            //    selectedTransactions = selectedTransactions.Where(t => t.TransId.Equals(searchString)).ToList();

            if (!string.IsNullOrEmpty(searchString))
                selectedTransactions = selectedTransactions.Where(t => t.TransId == transId).ToList();

            if (!string.IsNullOrEmpty(donorName))
                selectedTransactions = selectedTransactions.Where(t => Fuzz.Ratio(t.Donor.PrimaryName, donorName) > 50).ToList();

            if (!string.IsNullOrEmpty(campaignName))
                selectedTransactions = selectedTransactions.Where(t => Fuzz.Ratio(t.Campaign.CampaignName, campaignName) > 50).ToList();

            return View(selectedTransactions);
            //return selectedTransactions.Count != 0 ?
            //    View(selectedTransactions) :
            //    NotFound("Nothing Found. Please try again.");
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions
                .Include(t => t.Campaign)
                .Include(t => t.Donor)
                .FirstOrDefaultAsync(m => m.TransId == id);
            if (transactions == null)
            {
                return NotFound();
            }

            return View(transactions);
        }
                
        // GET: Transactions/Create
        public async Task<IActionResult> Create(TransactionsViewModel transactionsViewModel)
        {
            //var transactionModel = new Transactions();
            //transactionModel.listFlag = GetTransactionsFromDB();

            ViewData["CampaignId"] = new SelectList(_context.Campaign, "CampaignId", "CampaignCode");
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName");
            ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "PrimaryName");
            ViewData["BatchId"] = new SelectList(_context.Batches, "BatchId", "BatchId");
            ViewData["UserId"] = new SelectList(_manager.Users, "UserId", "NormalizedUserName");
            //ViewData["DonorFlags"] = await _context.DonorFlags.ToListAsync();
            //ViewData["DonorFlags"] = new SelectList(_context.DonorFlags, "FlagId", "FlagName");
            ViewData["FlagName"] = new SelectList(_context.DonorFlags, "FlagId", "FlagName");

            return View(transactionsViewModel);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionsViewModel transactionsViewModel, Transactions transactions/*, List<int> selectedFlagIds*/)
        {
            try
            {
                //Transactions transactions = new Transactions();
                //transactions.listFlag = 
                transactions.GiftAmount = transactionsViewModel.GiftAmount;
                transactions.PledgeAmt = transactionsViewModel.PledgeAmt;
                transactions.PledgeBalance = transactionsViewModel.PledgeBalance;
                transactions.CheckNumber = transactionsViewModel.CheckNumber;
                transactions.CheckRouteNum = transactionsViewModel.CheckRouteNum;
                transactions.CheckAcctNum = transactionsViewModel.CheckAcctNum;
                transactions.RecordType = transactionsViewModel.RecordType;
                transactions.GiftType = transactionsViewModel.GiftType;
                transactions.GiftSource = transactionsViewModel.GiftSource;
                transactions.SourceCode = transactionsViewModel.SourceCode;
                transactions.ProgramCode = transactionsViewModel.ProgramCode;
                transactions.PetitionFlag = transactionsViewModel.PetitionFlag;

                var userIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                var campaign = await _context.Campaign.Where(c => c.CampaignId == transactions.CampaignId).FirstOrDefaultAsync();
                var client = await _context.Client.Where(c => c.ClientId == transactions.ClientId).FirstOrDefaultAsync();
                var donor = await _context.Donor.Where(c => c.DonorId == transactions.DonorId).FirstOrDefaultAsync();
                var batches = await _context.Batches.Where(t => t.BatchId == transactions.BatchId).FirstOrDefaultAsync();

                //var flags = await _context.DonorFlags.ToListAsync();                

                //var user = await _manager.FindByIdAsync(userIdentifier);
                var user = await _manager.FindByIdAsync(transactionsViewModel.SelectedUserId);

                transactions.Campaign = campaign;
                transactions.Client = client;
                transactions.Donor = donor;
                transactions.Batches = batches;
                transactions.UserId = user.Id;
                string username = user.NormalizedUserName;
                //transactions.DonorFlags = flags;
                //transactions.DonorFlags = _context.DonorFlags.Where(flags => )

                //transactions.DonorFlagCheckBoxes = donorFlagCheckBoxes;
                //transactions.DonorFlagCheckBoxes = _context.DonorFlagCheckBoxes.Where(donorFlagCheckBoxes 
                //    => selectedDFCBIds.Contains(donorFlagCheckBoxes.DonorFlagCheckId)).ToList();
                
                ViewData["Batches"] = transactions;
                ViewBag.Batches = transactions.Batches;

                //ViewData["DonorFlags"] = transactions;
                //ViewBag.DonorFlags = transactions.DonorFlags;
                //user.ManagedClients.Add(transactions);

                _context.Transactions.Add(transactions);
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            
            return View(transactions);
        }
                
        /*
        public async Task<IActionResult> CreateShortcut()
        {

        }
        */
        public IActionResult ClientIndex()
        {
            var clients = _context.Client.ToList();
            return View(clients);
        }

        public IActionResult CreateClient()
        {
            ViewBag.Client = new SelectList(_context.Client, "ClientID", "ClientName");
            return View();
        }

        public IActionResult DonorIndex()
        {
            var donors = _context.Donor.ToList();
            return View(donors);
        }

        public IActionResult CreateDonor()
        {
            ViewBag.Donor = new SelectList(_context.Donor, "DonorId", "FirstName");
            return View();
        }
        [HttpPost]
        public IActionResult CreateDonor(Donor donor)
        {
            _context.Donor.Add(donor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CascadeList()
        {
            ViewBag.Client = new SelectList(_context.Client, "ClientId", "ClientName");
            return View();
        }

        public JsonResult LoadDonor(int Id)
        {
            var donor = _context.Donor.Where(d => d.DonorId == Id).ToList();
            return Json(new SelectList(donor, "DonorId", "FirstName"));
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions.FindAsync(id);
            
            if (transactions == null)
            {
                return NotFound();
            }

            //var dfcbOptions = await _context.DonorFlagCheckBoxes.ToListAsync();

            ViewData["CampaignId"] = new SelectList(_context.Campaign, "CampaignId", "CampaignId", transactions.CampaignId);
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode", transactions.ClientId);
            ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "DonorId", transactions.DonorId);
            ViewData["BatchId"] = new SelectList(_context.Batches, "BatchId", "BatchId", transactions.BatchId);
            //ViewData["DonorFlagCheckId"] = dfcbOptions;

            //var viewModel = new DFCheckBoxViewModel
            //{
            //    DFCBOptions = dfcbOptions
            //};

            //ViewBag.DFCBOptions = viewModel.DFCBOptions;
            //TempData["DFCBOptions"] = viewModel.DFCBOptions;

            return View(transactions);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransId,GiftAmount,GiftDate,PledgeAmt,PledgeBalance,CheckNumber,CheckRouteNum,CheckAcctNum,PostDate,RecordType,GiftType,TransDate,TransExpDate,UpdateDate,GiftSource,PetitionFlag,ClientId,DonorId,CampaignId,BatchId,BatchNumber,KeyCode,ListId,UserId")] Transactions transactions)
        {

            try
            {
                _context.Update(transactions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }
            
            return View();
            //if (id != transactions.TransId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(transactions);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!TransactionsExists(transactions.TransId))
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
            //ViewData["CampaignId"] = new SelectList(_context.Campaign, "CampaignId", "CampaignId", transactions.CampaignId);
            ////ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode", transactions.ClientId);
            //ViewData["DonorId"] = new SelectList(_context.Donor, "DonorId", "DonorId", transactions.DonorId);
            //return View(transactions);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transactions = await _context.Transactions
                .Include(t => t.Campaign)
                .Include(t => t.Donor)
                .FirstOrDefaultAsync(m => m.TransId == id);
            if (transactions == null)
            {
                return NotFound();
            }

            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions != null)
            {
                _context.Transactions.Remove(transactions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionsExists(int id)
        {
          return (_context.Transactions?.Any(e => e.TransId == id)).GetValueOrDefault();
        }
    }
}
