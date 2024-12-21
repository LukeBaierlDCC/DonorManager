using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.SelectHistory
{
    public class SelectHistory
    {
        [Key]
        [DisplayName("Order ID")]
        public int HistoryId { get; set; }

        public string OrderDescription { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public int ClientId { get; set; }
    }
}
