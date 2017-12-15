using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Temporary_Prison.Models
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Password { get;set;}
        public string[] Roles { get; set; }
    }
    public class UserRoles
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}