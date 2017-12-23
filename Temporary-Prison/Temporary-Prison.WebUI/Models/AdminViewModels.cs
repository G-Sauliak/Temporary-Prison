using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Temporary_Prison.Models
{
    public class CreateUserViewModel
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
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }


    public class EditUserViewModel
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
        [Display(Name = "New Password")]
        public string Password { get; set; }

        public UserAndRoles UserAndRoles { get; set; }
    }

    public class UserAndRoles
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
    }
}