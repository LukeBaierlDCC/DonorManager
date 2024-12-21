using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using DonorManager2024.Areas.Identity.Pages.Account;
using DonorManager.Models;

namespace DonorManager2024.Models
{
    //[Table("dbo.AspNetUsers")]
    public class AllUsers
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public string UserName { get; set; }
        public string EmailConfirmed { get; set; }
        public string SelectedRole { get; set; }
    }
}
