using DonorManager2024.Models;
using DonorManager.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DonorManager2024.Controllers
{
    public class DropDownListController : Controller
    {
        public ActionResult CascadingDropDownList()
        {
            return View();
        }

        
    }
}
