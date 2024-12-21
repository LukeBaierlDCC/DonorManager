using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.CampaignsRelated
{
    public class CampaignLists
    {
        [Key]
        public int CampaignListId { get; set; }

        public string ListDesc { get; set; }

        public string MailCount { get; set; }

        public string MailCost { get; set; }

        [ForeignKey(nameof(CampaignLists))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        //assigned to a person for the purpose of list manager, not for clients or donors to see how a person is linked to a campaign
        [ForeignKey(nameof(CampaignLists))]
        public int ListNameId { get; set; }

        [ForeignKey(nameof(CampaignLists))]
        public string ListName { get; set; }

        [ForeignKey(nameof(CampaignLists))]
        public string ListType { get; set; }

        [ForeignKey(nameof(CampaignLists))]
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }

        [ForeignKey(nameof(CampaignLists))]
        public int KeyCode { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime MailDate { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime UpdateDate { get; set; }
    }
}
