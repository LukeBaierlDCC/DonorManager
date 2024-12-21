using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.CampaignsRelated
{
    public class CampaignHistoryRollup
    {
        [Key]
        public string KeyCode { get; set; }

        public string GiftCount { get; set; }

        public string GiftAmount { get; set; }

        public DateTime GiftDate { get; set; }

        [ForeignKey(nameof(CampaignHistoryRollup))]
        public int ClientId { get; set; }
        //public Client Client { get; set; }

        [ForeignKey(nameof(CampaignHistoryRollup))]
        public int CampaignId { get; set; }
        //public Campaign Campaign { get; set; }
    }
}
