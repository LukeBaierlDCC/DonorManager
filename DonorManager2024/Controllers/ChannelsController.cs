using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonorManager2024.Data;
using DonorManager2024.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DonorManager.Models;
using FuzzySharp;
using Microsoft.AspNetCore.Authorization;

namespace DonorManager2024.Controllers
{
    public class ChannelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChannelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Channels
        [Authorize(Roles = "Admin, ABData")]
        public async Task<IActionResult> Index(string searchString, string sortOrder, string channelCode, string channelDesc)
        {            
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchString;
            ViewBag.CodeSort = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
                        
            var AllChannels = from ac in _context.Channels.Include(ac => ac.ChannelCode).Include(ac => ac.ChannelDesc) select ac;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.Users.Include(u => u.ManagedClients).Where(u => u.Id == userId).FirstOrDefault();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<int> allChannelIds = new List<int>();          
            
            var selectedChannels = userRoles.Contains("Admin") || userRoles.Contains("ABData")
                ? await _context.Channels.ToListAsync() 
                : await _context.Channels.Where(c => allChannelIds.Contains(c.ChannelId)).ToListAsync();

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterChannelCode"] = channelCode;
            ViewData["CurrentFilterChannelDesc"] = channelDesc;

            IQueryable<Channels> allChannels = from ac in _context.Channels select ac;

            switch (sortOrder)
            {
                case "code_desc":
                    AllChannels = AllChannels.OrderByDescending(ac=>ac.ChannelCode);
                    break;
                default:
                    AllChannels = AllChannels.OrderBy(ac => ac.ChannelCode);
                    break;
            }

            if (string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(channelCode)
                && string.IsNullOrEmpty(channelDesc))
                return View(selectedChannels.ToList());

            bool success = int.TryParse(searchString, out var channelId);
            if (!success)
                searchString = "";

            if (!string.IsNullOrEmpty(searchString))
                selectedChannels = selectedChannels.Where(c => c.ChannelId == channelId).ToList();

            if (!string.IsNullOrEmpty(channelCode))
                selectedChannels = selectedChannels.Where(d => Fuzz.Ratio(d.ChannelCode, channelCode) > 50).ToList();

            if (!string.IsNullOrEmpty(channelDesc))
                selectedChannels = selectedChannels.Where(d => Fuzz.Ratio(d.ChannelDesc, channelDesc) > 50).ToList();

            return selectedChannels.Count != 0 ?
                 View(selectedChannels) :
                 NotFound("No Channels Found, Bruh");
             
            /*
            return _context.Channels != null ?
                        View(await _context.Channels.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Channels'  is null.");
            */
        }

        // GET: Channels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Channels == null)
            {
                return NotFound();
            }

            var channels = await _context.Channels
                .FirstOrDefaultAsync(m => m.ChannelId == id);
            if (channels == null)
            {
                return NotFound();
            }

            return View(channels);
        }

        // GET: Channels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Channels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChannelId,ChannelCode,ChannelDesc")] Channels channels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(channels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(channels);
        }

        // GET: Channels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Channels == null)
            {
                return NotFound();
            }

            var channels = await _context.Channels.FindAsync(id);
            if (channels == null)
            {
                return NotFound();
            }
            return View(channels);
        }

        // POST: Channels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChannelId,ChannelCode,ChannelDesc")] Channels channels)
        {
            if (id != channels.ChannelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(channels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChannelsExists(channels.ChannelId))
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
            return View(channels);
        }

        // GET: Channels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Channels == null)
            {
                return NotFound();
            }

            var channels = await _context.Channels
                .FirstOrDefaultAsync(m => m.ChannelId == id);
            if (channels == null)
            {
                return NotFound();
            }

            return View(channels);
        }

        // POST: Channels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Channels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Channels'  is null.");
            }
            var channels = await _context.Channels.FindAsync(id);
            if (channels != null)
            {
                _context.Channels.Remove(channels);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChannelsExists(int id)
        {
          return (_context.Channels?.Any(e => e.ChannelId == id)).GetValueOrDefault();
        }
    }
}
