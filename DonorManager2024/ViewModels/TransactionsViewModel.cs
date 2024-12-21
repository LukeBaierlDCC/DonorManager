using DonorManager2024.Models.DonorRelated;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DonorManager2024.ViewModels
{
    public class TransactionsViewModel
    {
        public string SelectedUserId { get; set; } = string.Empty;

        public string GiftAmount { get; set; }

        public DateTime? GiftDate { get; set; }

        public string PledgeAmt { get; set; }

        public string PledgeBalance { get; set; }

        public string CheckNumber { get; set; }

        public string CheckRouteNum { get; set; }

        public string CheckAcctNum { get; set; }

        public DateTime PostDate { get; set; }

        public string RecordType { get; set; }

        public string GiftType { get; set; }

        public DateTime TransExpDate { get; set; }

        public string GiftSource { get; set; }

        public string SourceCode { get; set; }

        public string ProgramCode { get; set; }

        public string PetitionFlag { get; set; }

        public string DonorId { get; set; }

        public string CampaignId { get; set; }

        public string BatchId { get; set; }

        public string ClientId { get; set; }

        public string FlagId { get; set; }

        public List<DonorFlags> ListFlag { get; set; } = new List<DonorFlags>();
        public IEnumerable<SelectListItem> FlagListItems { get { return new SelectList(ListFlag, "FlagId", "FlagName"); } }

        //public List<DonorFlags> listFlag { get; set; }
        //public IEnumerable<SelectListItem> FlagListItems
        //{
        //    get { return new SelectList(listFlag, "FlagId", "FlagName"); }
        //}

    }
}
