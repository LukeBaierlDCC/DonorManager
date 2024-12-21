using DonorManager.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.Premiums
{
    public class TransPremiums
    {
        [ForeignKey(nameof(TransPremiums))]
        public int TransId { get; set; }
        public Transactions Transactions { get; set; } = null!;

        [ForeignKey(nameof(TransPremiums))]
        public int PremiumId { get; set; }
        public Premiums Premiums { get; set; } = null!;

        [ForeignKey(nameof(TransPremiums))]
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        [ForeignKey(nameof(TransPremiums))]
        public int DonorId { get; set; }
        public Donor Donor { get; set; } = null!;
    }
}
