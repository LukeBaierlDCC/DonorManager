using Microsoft.AspNetCore.Identity;
using DonorManager2024.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DonorManager2024.Areas.Identity.Pages.Account;


namespace DonorManager2024.ViewModels
{
    //[Table("dbo.AspNetUsers")]
    public class AllUsersViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string EmailConfirmed { get; set; }
        public string SelectedRole { get; set; }
        public string NormalizedName { get; set; }
    }
}
