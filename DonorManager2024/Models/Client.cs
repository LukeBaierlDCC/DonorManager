using DonorManager2024.Models;
using DonorManager2024.Models.DonorRelated;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager.Models
{
    public class Client
    {
        [Key]
        [DisplayName("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        [Required]
        [DisplayName("Code")]
        public string ClientCode { get; set; }
        [MaxLength(100)]
        [DisplayName("Name")]
        public string ClientName { get; set; }
        [MaxLength(100)]
        [DisplayName("Address")]
        public string ClientAddress { get; set; }
        [MaxLength(10)]
        [DisplayName("Phone Number")]
        public string ClientPhone { get; set; }
        [MaxLength(25)]
        [DisplayName("Contact 1")]
        public string Contact1 { get; set; }
        [MaxLength(25)]
        [DisplayName("Contact 2")]
        public string Contact2 { get; set; }
        [MaxLength(25)]
        [DisplayName("Trans Dialog")]
        public string TransDialog { get; set; }

        [DisplayName("Last Updated")]
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        //[ForeignKey(nameof(Client))]
        //public int DonorId { get; set; }        
        //public int CampaignId { get; set; }
        public ICollection<Donor> Donor { get; set; } = new List<Donor>();
        public ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
        //public ICollection<DonorFlags> DonorFlags { get; set; } = new List<DonorFlags>();
        //public ICollection<DonorFlagCheckBoxes> DonorFlagCheckBoxes { get; set; } = new List<DonorFlagCheckBoxes>();
    }
}
