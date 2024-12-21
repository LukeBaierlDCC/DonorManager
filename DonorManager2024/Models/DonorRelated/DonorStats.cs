using DonorManager.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.DonorRelated
{
    public class DonorStats
    {
        [ForeignKey(nameof(DonorStats))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(DonorStats))]
        public int DonorId { get; set; }
        public Donor Donor { get; set; }

        public string DonorFlag { get; set; }

        public string SourceCode { get; set; }

        public string HPG { get; set; }

        public string MRC { get; set; }

        public string Gifts { get; set; }

        public string LPG { get; set; }

        public string State { get; set; }

        public string ModelRank { get; set; }

        public string ModelProfit { get; set; }

        public string MultiType { get; set; }

        public string MembershipStart { get; set; }

        public string MembershipExp { get; set; }

        public string Online { get; set; }

        public string DM { get; set; }

        public string Event { get; set; }

        public string Grass { get; set; }

        public string TM { get; set; }

        public string OLStore { get; set; }

        public string AnnualDonor { get; set; }

        public string PCUpgrade { get; set; }

        public string DMUpgrade { get; set; }

        public string TotalIncome { get; set; }

        public DateTime MRCDate { get; set; }

        public DateTime FirstGiftDate { get; set; }

        [ForeignKey(nameof(DonorStats))]
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }
    }
}
