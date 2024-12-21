using Microsoft.AspNetCore.Http;
using DonorManager2024.ViewModels;
using DonorManager2024.Areas.Identity.Pages.Account;
using DonorManager2024.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DonorManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DonorManager2024.Models;
using System.Security.Claims;


namespace DonorManager2024.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AllUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AllUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: AllUsersController - currently only pulling from AspNetUsers, needs to pull from AspNetUserRoles
        public ActionResult Index()
        {
            var allUsers = _userManager.Users.ToList();

            var filteredUsers = allUsers.Where(u => u.EmailConfirmed == true);
            var viewModel = new AllUsers
            {
                Users = filteredUsers
            };
            return View(viewModel);
        }

        // GET: AllUsersController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        public async Task<IActionResult> Details(int? id/*, AllUsersViewModel allUsersViewModel*/)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.GetUserAsync(User);
            var client = await _context.Client.FirstOrDefaultAsync(c => c.ClientId == id);

            var userRoles = await _userManager.GetRolesAsync(user);

            if (user.ManagedClients.Contains(client) || userRoles.Contains("Admin"))
            {
                return View(client);
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        // GET: AllUsersController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AllUsersController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AllUsersController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AllUsersController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AllUsersController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AllUsersController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
