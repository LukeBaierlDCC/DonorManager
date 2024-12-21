using System.ComponentModel.DataAnnotations;

namespace DonorManager2024.Models.UsersRelated
{
    public class UserLevels
    {
        [Key]
        public int UserLvl { get; set; }

        public string LevelDesc { get; set; }
    }
}
