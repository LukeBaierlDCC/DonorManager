using System.ComponentModel.DataAnnotations;

namespace DonorManager2024.Models.DonorSelects
{
    public class DonorSelectCriteria
    {
        [Key]
        public int SelectId { get; set; }

        [Key]
        public int JobId { get; set; }

        public string KeyCode { get; set; }

        public string SelectCriteria { get; set; }

        public int SelectCount { get; set; }

        public string GroupCode { get; set; }

        public string Omit { get; set; }

        public string Priority { get; set; }
    }
}
