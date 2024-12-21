using DonorManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DonorManager2024.ViewModels
{
    public class CampaignViewModel
    {
        [Display(Name = "Campaign ID")]
        public int CampaignId { get; set; }
        [Display(Name = "Name of Campaign")]
        public string CampaignName { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime MailDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        [Display(Name = "Campaign Name Abbreviated")]
        public string CampaignCode { get; set; }
        [Display(Name = "Job Name")]
        public string JobName { get; set; }
        [Display(Name = "Promotion ID?")]
        public string PromotionId { get; set; }
        [Display(Name = "Channel ID?")]
        public string ChannelID { get; set; }
        [Display(Name = "Total Package Cost")]
        public string FinalPkgCost { get; set; }
        [Display(Name = "Client User")]
        //public int SelectedClientId { get; set; }
        public string SelectedUserId { get; set; } = string.Empty;
    }
}
