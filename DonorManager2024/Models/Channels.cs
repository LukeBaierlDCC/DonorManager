using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace DonorManager2024.Models
{
    public class Channels
    {
        [Key]
        public int ChannelId { get; set; }

        public string ChannelCode { get; set; }

        public string ChannelDesc { get; set; }

        
    }

    
}
