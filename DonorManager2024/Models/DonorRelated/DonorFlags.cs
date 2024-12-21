using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.DonorRelated
{
    public class DonorFlags
    {
        [Key]        
        public int FlagId { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        //[MinLength(2)]
        public string? Comment { get; set; }

        public string FlagName { get; set; }

        public bool Visibility { get; set; }                                      

        [ForeignKey(nameof(DonorFlags))]
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        
        public ICollection<Donor> Donors { get; set; }

        public ICollection<Transactions> Transactions { get; set; }

        public void DeepCopy(DonorFlags donorFlags)
        {            
            this.Comment = donorFlags.Comment;
            this.DateAdded = donorFlags.DateAdded;
            this.FlagId = donorFlags.FlagId;
            this.FlagName = donorFlags.FlagName;
            this.Visibility = donorFlags.Visibility;
        }

        //[ForeignKey(nameof(DonorFlags))]
        //public int DonorId { get; set; }
        //public Donor Donor { get; set; }
        //Donor will have a list of donor flags because donor will have a 1 to many

        //[ForeignKey(nameof(DonorFlags))]
        //public int FlagDtlId { get; set; }
        //public DonorFlagDetail DonorFlagDetail { get; set; }

        //public int FlagValue { get; set; } //set to a power of 2

        //public enum Flags : Int64
        //{
        //    None = 0,
        //    AnnualFlag = 1,
        //    MailFlag = 2,
        //    EmailFlag = 4,
        //    NoExchangeFlag = 8,
        //    DeceasedFlag = 16,
        //    NoRenewalFlag = 32,
        //    NoTMFlag = 64,
        //    WillFlag = 128,
        //    NoThankYouFlag = 256,
        //    PremiumDeclinedFlag = 512
        //}
    }
}
