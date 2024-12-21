using DonorManager.Models;
using DonorManager2024.Models.UsersRelated;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.MailGroup
{
    public class Nixies
    {
        //Name1 Prefix, First, Middle, Last, Suffix
        //Name2 Prefix, First, Middle, Last, Suffix
        //Company, Secondary, Primary, CSV
        [Key]
        public int NixieId { get; set; }
        //public string Name1 { get; set; }
        public string Prefix1 { get; set; }
        public string First1 { get; set; }
        public string Middle1 { get; set; }
        public string Last1 { get; set; }
        public string Suffix1 { get; set; }
        //public string Name2 { get; set; }
        public string Prefix2 { get; set; }
        public string First2 { get; set; }
        public string Middle2 { get; set; }
        public string Last2 { get; set; }
        public string Suffix2 { get; set; }
        public string Company { get; set; }
        public string Primary { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [ForeignKey(nameof(Nixies))]
        public string UserId { get; set; }
    }
}
