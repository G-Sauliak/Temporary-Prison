using System;
using System.ComponentModel.DataAnnotations;

namespace Temporary_Prison.Models
{
    public class SearchFilterModelView
    {
        [Display (Name = "Find by date of detention")]
        [DataType(DataType.Date)]
        public DateTime DateOfDetention { get; set; }

        [Display(Name = "Find by name")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        public string Name { get; set; }

        [Display(Name = "Find by Address")]
        [DataType(DataType.Text)]
        public string Address { get; set; }
    }
}