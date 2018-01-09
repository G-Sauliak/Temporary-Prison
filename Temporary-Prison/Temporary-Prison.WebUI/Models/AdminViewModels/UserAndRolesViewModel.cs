using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Temporary_Prison.Models
{
    public class UserAndRolesViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
    }
}