using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.History
{
    public class NameHistory
    {
        [Key]
        public int NameId { get; set; }

        [ForeignKey(nameof(NameHistory))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(NameHistory))]
        public int DonorId { get; set; }
        public Donor Donor { get; set; }

        [ForeignKey(nameof(NameHistory))]
        public int TransId { get; set; }
        public Transactions Transactions { get; set; }

        public string PName1 { get; set; }

        public string FName1 { get; set; }

        public string MName1 { get; set; }

        public string LName1 { get; set; }

        public string PName2 { get; set; }

        public string FName2 { get; set; }

        public string MName2 { get; set; }

        public string LName2 { get; set; }
    }
}
