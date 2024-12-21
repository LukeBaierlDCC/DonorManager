using System.ComponentModel.DataAnnotations;

namespace DonorManager2024.Models
{
    public class Promotions
    {
        [Key]
        public int PromotionId { get; set; }

        public string PromotionCode { get; set; }

        public string Description { get; set; }

        public string FundType { get; set; }
    }
}
