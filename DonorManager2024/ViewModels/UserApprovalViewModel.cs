using DonorManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DonorManager2024.ViewModels
{
    public class UserApprovalViewModel
    {
        //public List<ApplicationUser> UsersForApprovals { get; set; }
        [Display(Name = "ID")]
        public string UserId { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "RoleID")]
        public string SelectedRoleId { get; set; }

    }
}
