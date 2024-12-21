using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        public string MembershipDesc { get; set; }

        public string MembershipCode { get; set; }

        public string LoGift { get; set; }

        public string HiGift { get; set; }

        //[ForeignKey(nameof(Membership))]
        //public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}
