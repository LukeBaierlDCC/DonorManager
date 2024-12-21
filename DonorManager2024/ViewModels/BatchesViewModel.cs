using DonorManager.Models;
using DonorManager2024.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DonorManager2024.ViewModels
{
    public class BatchesViewModel
    {
        [AllowNull]
        [Display(Name = "Client User Account")]
        public int SelectedClientId { get; set; }

        public Batches Batches { get; set; }

        //public int BatchId { get; set; }

        [Display(Name = "Batch Number")]
        public string BatchNum { get; set; }

        [Display(Name = "Promotion Category")]
        public string PromoCat { get; set; }

        [Display(Name = "Batch Count")]
        public string BatchCount { get; set; }

        [Display(Name = "Batch Amount")]
        public string BatchAmount { get; set; }

        [Display(Name = "Actual Count")]
        public string ActualCount { get; set; }

        [Display(Name = "Actual Amount")]
        public string ActualAmount { get; set; }

        [Display(Name = "Deposit Error")]
        public string DepositError { get; set; }

        [Display(Name = "Tender Type ID")]
        public string TenderTypeID { get; set; }

        [DataType(DataType.Date)]
        public DateTime BatchDate { get; set; } = DateTime.Now;

        public Client Client { get; set; }

        //public int ClientId { get; set; }
    }
}
