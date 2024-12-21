using DonorManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DonorManager2024.ViewModels
{
    public class CreateClientViewModel
    {
        [Display(Name = "Client User Account")]
        public string SelectedUserId { get; set; } = string.Empty;

        [Display(Name = "Your Full Name")]
        public string ClientName { get; set; }

        [Display(Name = "Business Address")]
        public string ClientAddress { get; set; }

        [Display(Name = "Business Phone")]
        public string ClientPhone { get; set; }

        [Display(Name = "Optional Backup Contact 1")]
        public string Contact1 { get; set; }

        [Display(Name = "Optional Backup Contact 2")]
        public string Contact2 { get; set; }

        [Display(Name = "Transaction Dialog? Leave Blank If Unknown")]
        public string TransDialog {  get; set; }

        [Display(Name = "Your Company Name Abbreviated")]
        public string ClientCode { get; set; }

        //[Display(Name = "Donor Flags")]
        //public List<DonorFlagViewModel> DonorFlags { get; set; }

    }
}
