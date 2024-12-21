using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.History
{
    //public class AddressHistory
    //{
    //    [Key]
    //    public int AddressId { get; set; }

    //    public string Firm { get; set; }

    //    public string SecAddress { get; set; }

    //    public string PrimAddress { get; set; }

    //    public string City { get; set; }

    //    public string State { get; set; }

    //    public string ZIP { get; set; }

    //    public string Country { get; set; }

    //    public bool Mailable { get; set; }

    //    public string NCOARC { get; set; }

    //    public DateTime MoveDate { get; set; }

    //    public string MoveType { get; set; }

    //    //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    //    //public DateTime UpdateDate { get; set; } = DateTime.Now;

    //    [ForeignKey(nameof(AddressHistory))]
    //    public int ClientId { get; set; }
    //    public Client Client { get; set; }

    //    [ForeignKey(nameof(AddressHistory))]
    //    public int DonorId { get; set; }
    //    public Donor Donor { get; set; }

    //    [ForeignKey(nameof(AddressHistory))]
    //    public int TransId { get; set; }
    //    //public Transactions Transactions { get; set; }
    //}
}
