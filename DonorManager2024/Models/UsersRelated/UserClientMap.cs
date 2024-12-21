using DonorManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager2024.Models.UsersRelated
{
    public class UserClientMap
    {
        [Key]
        public int UserClientId { get; set; }

        [ForeignKey(nameof(UserClientMap))]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
