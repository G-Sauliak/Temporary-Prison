using System.ComponentModel.DataAnnotations;


namespace Temporary_Prison.Models
{
    public class UserViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get;set;}
        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
    }


    public class UserRoles
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }

    public class UserAndRolesViewModel
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}