using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.DonorRelated
{
    public class DonorModel
    {
        [ForeignKey(nameof(DonorModel))]
        public int DonorId { get; set; }

        [ForeignKey(nameof(DonorModel))]
        public int ClientId { get; set; }

        public string AcqYear { get; set; }

        public string AcqChannel { get; set; }

        public string Rank { get; set; }

        public string ProfitFlag { get; set; }
    }
}
