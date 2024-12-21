using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.SelectHistory
{
    public class SelectHistoryDetail
    {
        [Key]
        public int HistoryDetailId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public int OrderId { get; set; }

        public int DonorId { get; set; }

        public int TransId { get; set; }

        public int KeyCode { get; set; }
    }
}
