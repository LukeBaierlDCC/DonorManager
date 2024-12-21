using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DonorManager2024.Models;
using DonorManager2024.Models.DonorRelated;
using DonorManager2024.Models.History;
using Microsoft.EntityFrameworkCore;

namespace DonorManager.Models
{
    public class Donor
    {
        [Key]
        [DisplayName("Donor ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DonorId { get; set; }
        //public int UserId { get; set; }
        [MaxLength(100)]
        [DisplayName("Full Name")]
        public string PrimaryName { get; set; }
        [MaxLength(100)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [MaxLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [MaxLength(100)]
        [DisplayName("Secondary Name")]
        public string SecondaryName { get; set; }
        [MaxLength(50)]
        [DisplayName("Firm")]
        public string Firm {  get; set; }
        [MaxLength(75)]
        [DisplayName("Primary Address")]
        public string PrimaryAddress {  get; set; }
        [MaxLength(50)]
        [DisplayName("Secondary Address")]
        public string SecondAddress { get; set; }
        [MaxLength(55)]
        public string City { get; set; }
        [MaxLength(55)]
        public string State { get; set; }
        [MaxLength(15)]
        public string Zip {  get; set; }
        [MaxLength(60)]
        public string Country { get; set; }
        [MaxLength(15)]
        public string Phone { get; set; }
        [MaxLength(25)]
        [DisplayName("Work Phone Number")]
        public string WorkPhone { get; set; }
        [MaxLength(10)]
        [DisplayName("Extension")]
        public string WorkPhoneExt { get; set; }
        [MaxLength(15)]
        [DisplayName("Fax Number")]
        public string PhoneFax { get; set; }
        [MaxLength(50)]
        [DisplayName("Name of Employer")]
        public string Employer { get; set; }
        [MaxLength(50)]
        [DisplayName("Occupation")]
        public string Occupation { get; set; }
        [MaxLength(55)]
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("High Gift Amount")]
        public string HiGiftAmt { get; set; } = string.Empty;
        [DisplayName("Total Gift Amount")]
        public string TotalGiftAmt { get; set; } = string.Empty;
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Date Joined")]
        [DataType(DataType.Date)]
        public DateTime? JoinDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime MRCDate { get; set; }

        [ForeignKey(nameof(Donor))]
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public ICollection<DonorFlags> DonorFlags { get; set; } = new List<DonorFlags>();
        public int FlagId { get; set; }
        //public ICollection<DonorFlagCheckBoxes> DonorFlagCheckBoxes { get; set; } = new List<DonorFlagCheckBoxes>();

        public ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();

        //public ICollection<AddressHistory> AddressHistories { get; set; } = new List<AddressHistory>();

        public void DeepCopy(Donor donor)
        {
            this.PrimaryName = donor.PrimaryName;
            this.FirstName = donor.FirstName;
            this.MiddleName = donor.MiddleName;
            this.LastName = donor.LastName;
            this.SecondaryName = donor.SecondaryName;
            this.Firm = donor.Firm;
            this.PrimaryAddress = donor.PrimaryAddress;
            this.SecondAddress = donor.SecondAddress;
            this.City = donor.City;
            this.State = donor.State;
            this.Zip = donor.Zip;
            this.Country = donor.Country;
            this.Phone = donor.Phone;
            this.WorkPhone = donor.WorkPhone;
            this.WorkPhoneExt = donor.WorkPhoneExt;
            this.PhoneFax = donor.PhoneFax;
            this.Employer = donor.Employer;
            this.Occupation = donor.Occupation;
            this.Email = donor.Email;
            this.HiGiftAmt = donor.HiGiftAmt;
            this.TotalGiftAmt = donor.TotalGiftAmt;
            this.DateOfBirth = donor.DateOfBirth;
            this.JoinDate = donor.JoinDate;
            this.MRCDate = donor.MRCDate;
            this.DonorFlags = donor.DonorFlags;
        }
    }
}
