using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.UsersRelated
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string DomainUserName { get; set; }

        [ForeignKey(nameof(Users))]
        public int UserLvl { get; set; }
        //public UserLevels UserLevels { get; set; }
    }
}
