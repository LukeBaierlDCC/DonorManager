using DonorManager.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.DonorSelects
{
    public class DonorSelectKeys
    {
        [ForeignKey(nameof(DonorSelectKeys))]
        public int JobId { get; set; }
        public DonorSelectJobs DonorSelectJobs { get; set; }

        [ForeignKey(nameof(DonorSelectKeys))]
        public int SelectId { get; set; }
        public DonorSelectCriteria DonorSelectCriteria { get; set; }

        [ForeignKey(nameof(DonorSelectKeys))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey(nameof(DonorSelectKeys))]
        public int DonorId { get; set; }
        public Donor Donor { get; set; }
    }
}
