using DonorManager2024.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonorManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        //[Required]
        //public int AppUserId { get; set; }
        //[Required]
        //public string AppUserName { get; set; }
        //[Required(ErrorMessage = "E-mail is not valid")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,5}$")]
        //public string AppUserEmail { get; set; }
        //[Required]
        //public string Password { get; set; }

        //[Display(Name = "User Role")]
        //[Required(ErrorMessage = "User requires a role.")]
        //[EnumDataType(typeof(UserRole))]
        //public UserRole Role { get; set; }
        
        public ICollection<Client> ManagedClients { get; set; } = new List<Client>();
        public ICollection<Donor> Donors { get; set; }/* = new List<Donor>();*/

        //[ForeignKey(nameof(Client))]
        //public int ClientId { get; set; }
        [NotMapped]
        [Display(Name = "Role")]
        public string? SelectedRole { get; set; }
    }

    public enum UserRole
    {
        Admin,
        ABData,
        Client
    }
}
