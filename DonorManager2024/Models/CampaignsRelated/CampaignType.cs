using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.CampaignsRelated
{
    public class CampaignType
    {
        [Key]
        public int CampaignTypeId { get; set; }

        public string CampaignTypeDesc { get; set; }

        [ForeignKey(nameof(CampaignType))]
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }

        [ForeignKey(nameof(CampaignType))]
        public string ClientId { get; set; }
        public Client Client { get; set; }
    }
}
