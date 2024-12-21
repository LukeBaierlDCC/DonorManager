using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models
{
    public class Batches
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchId { get; set; }

        public string BatchNum { get; set; }

        public string PromoCat { get; set; }

        public string BatchCount { get; set; }

        public string BatchAmount { get; set; }

        public string ActualCount { get; set; }

        public string ActualAmount { get; set; }

        public string DepositError { get; set; }

        [Display(Name = "A/R")]
        public string AR { get; set; }

        [Display(Name = "Tender")]
        public string TenderTypeID { get; set; }
                
        //[DataType(DataType.Date)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime BatchDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(BatchId))]
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
    }
}
