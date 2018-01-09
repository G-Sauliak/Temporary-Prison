using System.ComponentModel.DataAnnotations;

namespace Temporary_Prison.Models
{
    public class EditUserViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        public UserAndRolesViewModel UserAndRoles { get; set; }
    }
}