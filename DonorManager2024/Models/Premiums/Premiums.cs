using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.Premiums
{
    public class Premiums
    {
        [Key]
        public int PremiumId { get; set; }
        public string PremiumCode { get; set; }
        public string PremiumDesc { get; set; }

        [ForeignKey(nameof(Premiums))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}
