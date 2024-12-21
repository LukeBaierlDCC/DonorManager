using DonorManager.Models;
using DonorManager2024.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using DonorManager2024.ViewModels;
using DonorManager2024.Areas.Identity.Pages.Account;

namespace DonorManager2024.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApprovalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApprovalController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role)
        {
            _manager = userManager;
            _context = context;
            _roleManager = role;
        }

        public IActionResult Index()
        {
            var users = _manager.Users.Where(u => u.EmailConfirmed == false).ToList();
            var roles = _roleManager.Roles.Where(r => !r.Name.Equals("Admin") && !r.Name.Equals("SuperAdmin"));
            List<IdentityRole> identityRoles = new List<IdentityRole>();
            identityRoles.Add(new IdentityRole());
            foreach (var role in roles)
            {
                identityRoles.Add(role);
            }
            ViewBag.Roles = new SelectList(identityRoles, "Id", "Name", null);

            List<UserApprovalViewModel> userApprovalViewModels = new List<UserApprovalViewModel>();

            foreach (var user in users)
            {
                UserApprovalViewModel uavm = new UserApprovalViewModel();
                uavm.UserId = user.Id;
                uavm.Email = user.Email;
                userApprovalViewModels.Add(uavm);
            }
            return View(userApprovalViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRole(List<UserApprovalViewModel> userApprovalViewModel)
        {
            try
            {
                foreach (UserApprovalViewModel userApprovalView in userApprovalViewModel)
                {
                    
                    var user = await _manager.FindByIdAsync(userApprovalView.UserId);
                    //check if the role exists or not, only confirm if the role exists
                    var _role = await _roleManager.FindByIdAsync(userApprovalView.SelectedRoleId);
                    if (_role == null)
                    {
                        continue;
                    }
                    await _manager.AddToRoleAsync(user, _role.NormalizedName);
                    user.EmailConfirmed = true;
                    await _manager.UpdateAsync(user);
                    
                    
                }
                

            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _manager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _manager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }
    }
}
