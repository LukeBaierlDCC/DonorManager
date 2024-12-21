using DonorManager.Models;
using DonorManager2024.Models.UsersRelated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.MailGroup
{
    public class NoMail
    {
        [Key]
        public int NoMailId { get; set; }

        //[ForeignKey(nameof(NoMail))]
        public Client Client { get; set; } = null!;
        public int? ClientId { get; set; }
        //public Client Client { get; set; }

        //[ForeignKey(nameof(NoMail))]
        public Users Users { get; set; }
        public int? UserId { get; set; }
        
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
    }
}
