using System.ComponentModel.DataAnnotations;

namespace DonorManager2024.Models
{
    public class ListNames
    {
        [Key]
        public int ListNameId { get; set; }

        public string ListName { get; set; }

        public string ListType { get; set; }
    }
}
