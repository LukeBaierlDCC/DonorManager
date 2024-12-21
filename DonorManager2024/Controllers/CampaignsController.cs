using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models;
using DonorManager2024.Models.DonorRelated;
using System.Security.Claims;
using DonorManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DonorManager2024.ViewModels;
using CsvHelper.Configuration.Attributes;
using CsvHelper;
using System.Globalization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Data;
using MiniExcelLibs;
using System.Text.RegularExpressions;
using DonorManager2024.Models.CampaignsRelated;
using CsvHelper.Configuration;
using FuzzySharp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DonorManager2024.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CampaignsController(ApplicationDbContext context, UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _manager = manager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = "Admin, ABData, Client")]
        [HttpGet]
        public async Task<IActionResult> Index(string? searchString = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _manager.Users.Include(c => c.ManagedClients).Where(u => u.Id == userId).FirstOrDefault();
            var userRoles = await _manager.GetRolesAsync(user);
            //Campaign campaign = new Campaign();
            
            List<int> ClientIds = new List<int>();
            foreach (var Client in user.ManagedClients)
            {
                ClientIds.Add(Client.ClientId);
            }

            ////var selectedCamps = await _context.Campaign.Include(C => C.Client).ToListAsync();
            ////var selectedCampaigns = await _context.Campaign.Include(c => c.Client).Where(c => c.Client.ClientId.ToString() == userId).ToListAsync();
            
            //string campName = campaign.CampaignName;
            //string campCode = campaign.CampaignCode;
            //string jobName = campaign.JobName;
            //string finalPkgCost = campaign.FinalPkgCost;

            var selectedCampaigns = userRoles.Contains("Admin") || userRoles.Contains("ABData")
                ? await _context.Campaign.ToListAsync()
                : await _context.Campaign.Include(c => c.Client).Where(c => ClientIds.Contains(c.ClientId)).ToListAsync();

            ViewData["CurrentFilter"] = searchString;
            //ViewData["CurrentFilterCampName"] = campName;
            //ViewData["CurrentFilterCampCode"] = campCode;
            //ViewData["CurrentFilterJobName"] = jobName;
            //ViewData["CurrentFilterFinalCost"] = finalPkgCost;

            //var applicationDbContext = _context.Campaign.Include(c => c.Client);

            if (string.IsNullOrEmpty(searchString)/* && string.IsNullOrEmpty(campName)
                && string.IsNullOrEmpty(campCode) && string.IsNullOrEmpty(jobName)
                && string.IsNullOrEmpty(finalPkgCost)*/)
                return View((selectedCampaigns));

            if (!string.IsNullOrEmpty(searchString))
                selectedCampaigns = selectedCampaigns.Where(C => C.CampaignCode.Equals(searchString)).ToList();

            //if (!string.IsNullOrEmpty(searchString))
            //    selectedCampaigns = selectedCampaigns.Where(C => C.CampaignName.Equals(searchString)).ToList();

            //if (!string.IsNullOrEmpty(searchString))
            //    selectedCampaigns = selectedCampaigns.Where(C => C.JobName.Equals(searchString)).ToList();

            //if (!string.IsNullOrEmpty(searchString))
            //    selectedCampaigns = selectedCampaigns.Where(C => C.FinalPkgCost.Equals(searchString)).ToList();

            return View(selectedCampaigns);
        }

        [Authorize(Roles = "Admin, ABData, Client")]
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            #region Upload CSV
            string fileName = $"{hostingEnvironment.WebRootPath}\\CSVFiles\\{file.FileName}";
            FileStream fileStream = System.IO.File.Create(fileName);
            
                file.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Seek(0, SeekOrigin.Begin);

            #endregion
            var campaigns = GetCampaignList(fileStream);


            foreach (var campaign in campaigns)
            {
                //check if null and if null - don't add to database but pop-up dialogue box and return to index                                
                _context.Campaign.Add(campaign);                
            }

            await _context.SaveChangesAsync();

            //Success message in TempData
            //TempData["SuccessMessage"] = "Mission accomplished!";
            //return RedirectToAction(nameof(Index));
            return await Index();
            //return View();
            
        }

        private List<Campaign> GetCampaignList(FileStream file)
        {
            List<Campaign> campaigns = new List<Campaign>();

            #region Read CSV
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {               
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var campaignCSV = csv.GetRecord<CampaignCSV>();
                    //new code begins
                    if (!IsValidCampaignCSV(campaignCSV))
                    {
                        ModelState.AddModelError("File", "Invalid data found in CSV. Please correct the data.");
                        return campaigns;
                    }
                    //new code ends
                    var campaign = new Campaign();
                    var promotion = _context.Promotions.Where(p => p.PromotionCode.Equals(campaignCSV.PromotionCode)).FirstOrDefault();
                    var channel = _context.Channels.Where(c => c.ChannelCode.Equals(campaignCSV.ChannelCode)).FirstOrDefault();
                    var client = _context.Client.Where(c => c.ClientName.Equals(campaignCSV.ClientName)).FirstOrDefault();                    

                    campaign.CampaignName = campaignCSV.CampaignName;
                    campaign.CampaignCode = campaignCSV.CampaignCode;
                    campaign.JobName = campaignCSV.JobName;
                    campaign.FinalPkgCost = campaignCSV.FinalPkgCost;
                    campaign.Client = client;
                    campaign.Promotions = promotion;
                    campaign.Channels = channel;
                                        
                    campaigns.Add(campaign);
                }

                //var configuration = new CsvConfiguration(CultureInfo.InvariantCulture) //<-this code may have been preventing correct assignment of Channel/PromotionIds
                //{
                //    Delimiter = ",",
                //    Comment = '%'
                //};
            }
            #endregion

            //#region Create CSV
            //path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\CSVFiles"}";
            //using (var write = new StreamWriter(path + "\\NewFile.csv"))
            //using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
            //{
            //    csv.WriteRecords(campaigns);
            //}
            //#endregion

            return campaigns;
        }

        private bool IsValidCampaignCSV(CampaignCSV campaignCSV)
        {
            if (string.IsNullOrWhiteSpace(campaignCSV.CampaignCode))
            {
                ModelState.AddModelError("CampaignCode", "CampaignCode is required.");
                return false;
            }

            //var existingCampaign = _context.Campaign.FirstOrDefault(c => c.CampaignCode.Equals(campaignCSV.CampaignCode));
            //if (existingCampaign == null)
            //{
            //    ModelState.AddModelError("CampaignCode", "Invalid CampaignCode.");
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(campaignCSV.ClientName))
            {
                ModelState.AddModelError("ClientName", "ClientName is required.");
                return false;
            }

            var existingClient = _context.Client.FirstOrDefault(c => c.ClientName.Equals(campaignCSV.ClientName));
            if (existingClient == null)
            {
                ModelState.AddModelError("ClientName", "Invalid ClientName.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(campaignCSV.ChannelCode))
            {
                ModelState.AddModelError("ChannelCode", "ChannelCode is required.");
                return false;
            }

            var existingChannel = _context.Channels.FirstOrDefault(p => p.ChannelCode.Equals(campaignCSV.ChannelCode));
            if (existingChannel == null)
            {
                ModelState.AddModelError("ChannelCode", "Invalid ChannelCode.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(campaignCSV.PromotionCode))
            {
                ModelState.AddModelError("PromotionCode", "PromotionCode is required.");
                return false;
            }

            var existingPromotion = _context.Promotions.FirstOrDefault(p => p.PromotionCode.Equals(campaignCSV.PromotionCode));
            if (existingPromotion == null)
            {
                ModelState.AddModelError("PromotionCode", "Invalid PromotionCode.");
                return false;
            }         
            return true;
        }

        // GET: Campaigns/Details/5
        [Authorize(Roles = "Admin, ABData, Client")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campaign == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.CampaignId == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // GET: Campaigns/Create
        [Authorize(Roles = "Admin, ABData, Client")]
        public IActionResult Create()
        {
            
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName");
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionCode");
            ViewData["ChannelId"] = new SelectList(_context.Channels, "ChannelId", "ChannelCode");
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Campaign campaign)
        {
            try
            {
                var promotion = await _context.Promotions.Where(p => p.PromotionId == campaign.PromotionId).FirstOrDefaultAsync();
                var channel = await _context.Channels.Where(c => c.ChannelId == campaign.ChannelID).FirstOrDefaultAsync();
                var client = await _context.Client.Where(c => c.ClientId == campaign.ClientId).FirstOrDefaultAsync();

                campaign.Client = client;
                campaign.Promotions = promotion;
                campaign.Channels = channel;
                _context.Campaign.Add(campaign);                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "404 ERROR! " + "Your data failed to save. " + "Please contact your system administrator.");
            }
            
            return View(campaign);
            //if (ModelState.IsValid)
            //{
            //    _context.Add(campaign);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode", campaign.ClientId);
            //ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionCode", campaign.PromotionId);
            //ViewData["ChannelID"] = new SelectList(_context.Channels, "ChannelID", "ChannelCode", campaign.ChannelID);
            //return View(campaign);
        }

        // GET: Campaigns/Edit/5
        [Authorize(Roles = "Admin, ABData")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campaign == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign.FindAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientName", campaign.ClientId);
            ViewData["PromotionId"] = new SelectList(_context.Promotions, "PromotionId", "PromotionCode", campaign.PromotionId);
            ViewData["ChannelId"] = new SelectList(_context.Channels, "ChannelId", "ChannelCode", campaign.ChannelID);
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,/* [Bind("CampaignId,CampaignName,CampaignCode,JobName,FinalPkgCost,MailDate,UpdateDate,ClientId,PromotionId,ChannelID")]*/ Campaign campaign)
        {
            try
            {
                //var campaignDb = _context.Campaign.Include(c => c.Channels).Where(c => c.CampaignId == id).FirstOrDefault();
                var promotion = await _context.Promotions.Where(p => p.PromotionId == campaign.PromotionId).FirstOrDefaultAsync();
                var channel = await _context.Channels.Where(p => p.ChannelId == campaign.ChannelID).FirstOrDefaultAsync();
                var client = await _context.Client.Where(p => p.ClientId == campaign.ClientId).FirstOrDefaultAsync();

                //campaignDb.DeepCopy(campaign);
                campaign.Client = client;
                campaign.Promotions = promotion;
                campaign.Channels = channel;
                _context.Update(campaign);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }
            return View(campaign);
            //if (id != campaign.CampaignId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(campaign);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CampaignExists(campaign.CampaignId))
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
            //ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientCode", campaign.ClientId);
            //return View(campaign);
        }

        // GET: Campaigns/Delete/5
        [Authorize(Roles = "Admin, ABData")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campaign == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaign
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.CampaignId == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Campaign == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Campaign'  is null.");
            }
            var campaign = await _context.Campaign.FindAsync(id);
            if (campaign != null)
            {
                _context.Campaign.Remove(campaign);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
          return (_context.Campaign?.Any(e => e.CampaignId == id)).GetValueOrDefault();
        }
    }
}
