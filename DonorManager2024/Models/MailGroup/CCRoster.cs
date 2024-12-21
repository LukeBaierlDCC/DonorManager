using DonorManager.Models;
using DonorManager2024.Models.UsersRelated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.MailGroup
{
    public class CCRoster
    {
        [Key]
        public int CCRosterId { get; set; }

        [ForeignKey(nameof(CCRoster))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        //[ForeignKey(nameof(CCRoster))]
        //public int DonorId { get; set; }
        //public Donor Donor { get; set; }

        [ForeignKey(nameof(CCRoster))]
        public int UserId { get; set; }
        //public Users Users { get; set; }

        public string PName1 { get; set; }

        public string FName1 { get; set; }

        public string MName1 { get; set; }

        public string LName1 { get; set; }

        public string PName2 { get; set; }

        public string FName2 { get; set; }

        public string MName2 { get; set; }

        public string LName2 { get; set; }

        public string Firm { get; set; }

        public string PrimAddress { get; set; }

        public string SecAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZIP { get; set; }

        public string CC { get; set; }

        public string CCType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "CC Expiration Date")]
        public DateTime CCExpDate { get; set; }

        public string GiftAmount { get; set; }

        [ForeignKey(nameof(CCRoster))]
        public string KeyCode { get; set; }

        public enum Types
        {
            Visa,
            Mastercard,
            Discover,
            AmericanExpress
        }
    }
}
