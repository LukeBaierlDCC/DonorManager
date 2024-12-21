using DonorManager.Models;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DonorManager2024.Models
{
    public class Campaign
    {
        [Key]
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int CampaignId { get; set; }

        [Index(0)]
        [DisplayName("Campaign Name")]
        public string CampaignName { get; set; } = "";

        [Index(1)]
        [DisplayName("Campaign Code")]
        public string CampaignCode { get; set; } = "";

        [Index(2)]
        [DisplayName("Job Name")]
        public string JobName { get; set; } = "";

        [Index(3)]
        [DisplayName("Final Package Cost")]
        public string FinalPkgCost { get; set; } = "";

        [Index(4)]
        [DataType(DataType.Date)]
        public DateTime MailDate { get; set; } = DateTime.Now;

        [Optional]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
                
        [Index(5)]
        [DisplayName("Client")]
        [ForeignKey(nameof(Campaign))]
        public int ClientId { get; set; }
        [Optional]
        [Ignore]
        public Client? Client { get; set; } = null!;

        [Index(6)]
        [DisplayName("Promotions")]
        [ForeignKey(nameof(Campaign))]        
        public int PromotionId { get; set; }
        public Promotions? Promotions { get; set; } = null!;

        [Index(7)]
        [DisplayName("Channels")]
        [ForeignKey(nameof(Campaign))]
        public int ChannelID { get; set; }
        public Channels? Channels { get; set; } = null!;

        public ICollection<Transactions> Transactions { get; set; }

        public void DeepCopy(Campaign campaign)
        {
            this.CampaignName = campaign.CampaignName;
            this.CampaignCode = campaign.CampaignCode;
            this.JobName = campaign.JobName;
            this.FinalPkgCost = campaign.FinalPkgCost;
            this.MailDate = campaign.MailDate;
            this.Client = campaign.Client;
            this.Promotions = campaign.Promotions;
            this.Channels = campaign.Channels;
        }
    }
}
