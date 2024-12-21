using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.DonorRelated
{
    public class DonorFlagDetail
    {
        [Key]
        public int FlagDtlId { get; set; }

        public string Code { get; set; }

        [MinLength(5)]
        public string Description { get; set; }

        //[ForeignKey(nameof(DonorFlagDetail))]
        //public int ClientId { get; set; }
        //public Client Client { get; set; }
    }
}
