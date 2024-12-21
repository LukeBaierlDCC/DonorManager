using DonorManager.Models;
using DonorManager2024.Models.UsersRelated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.DonorSelects
{
    public class DonorSelectJobs
    {
        [Key]
        public int JobId { get; set; }

        public string JobName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime JobDate { get; set; }

        [ForeignKey(nameof(DonorSelectJobs))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(DonorSelectJobs))]
        public int UserId { get; set; }
        public Users Users { get; set; }
    }
}
