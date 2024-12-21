using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.CodeAnalysis;
using DonorManager.Models;
using Microsoft.AspNetCore.Mvc;
using DonorManager2024.Models.DonorRelated;

namespace DonorManager2024.Models
{
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransId { get; set; }
        
        public string GiftAmount { get; set; }

        [Display(Name = "Gift Date")]
        [AllowNull]
        [DataType(DataType.Date)]
        public DateTime GiftDate { get; set; } = DateTime.Now;

        public string PledgeAmt { get; set; }

        public string PledgeBalance { get; set; }

        public string CheckNumber { get; set; }

        public string CheckRouteNum { get; set; }

        public string CheckAcctNum { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; } = DateTime.Now;

        public string RecordType { get; set; }

        public string GiftType { get; set; }

        [DataType(DataType.Date)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime TransDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime TransExpDate { get; set; }

        [DataType(DataType.Date)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        //------------------------------------------------------//
        [Display(Name = "Gift")]
        public string GiftSource { get; set; }
        [Display(Name = "Source Code")]
        public string SourceCode { get; set; }
        [Display(Name = "Program Code")]
        public string ProgramCode { get; set; }
        //------------------------------------------------------//
        public string PetitionFlag { get; set; }

        //[NotMapped]
        //[ForeignKey(nameof(Transactions))]
        public virtual Client Client { get; set; } = null!;
        public int? ClientId { get; set; } //already used as a foreign key

        ////[NotMapped]
        //[ForeignKey(nameof(Transactions))]
        public Donor Donor { get; set; } = null!;
        public int? DonorId { get; set; } //already used as a foreign key

        //[NotMapped]
        //[ForeignKey(nameof(Transactions))]
        public int CampaignId { get; set; } //already used as a foreign key
        public virtual Campaign Campaign { get; set; } = null!;

        //[NotMapped]
        //[ForeignKey(nameof(Transactions))]
        public int BatchId { get; set; } //not already used as a foreign key
        public virtual Batches Batches { get; set; } = null!;

        //public string BatchNumber { get; set; }

        /*
        public string AR { get; set; }        
        public string Tender { get; set; }        
        */

        //[NotMapped]
        //[ForeignKey(nameof(Transactions))]
        //public int KeyCode { get; set; }

        ////[NotMapped]
        //[ForeignKey(nameof(Transactions))]
        //public int ListId { get; set; }

        //[NotMapped]
        [ForeignKey(nameof(Transactions))]
        public string UserId { get; set; }

        //public int DonorFlagCheckId { get; set; }
        public ICollection<DonorFlags> DonorFlags { get; set; } = new List<DonorFlags>();
        public int FlagId { get; set; }
        //public List<DonorFlags> listFlag { get; set; }
        //public IEnumerable<SelectListItem> FlagListItems
        //{
        //    get { return new SelectList(listFlag, "FlagId", "FlagName"); }
        //}

        //public ICollection<DonorFlagCheckBoxes> DonorFlagCheckBoxes { get; set; }
        //public ICollection<DFCBTransaction> DFCBTransaction { get; set; }
        //[ForeignKey(nameof(Transactions))]
        //public int FlagId { get; set; }
        //public string? FlagName { get; set; }
    }
}
