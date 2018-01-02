using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temporary_Prison.Models
{
    public class RegistrationOfDetentionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PrisonerID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Detention")]
        public DateTime DateOfDetention { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Arrival")]
        public DateTime DateOfArrival { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Place Of Detention")]
        public string PlaceofDetention { get; set; }

        public EmployeeViewModel DetainedEmployee { get; set; }
        public EmployeeViewModel DeliveredEmployee { get; set; }
    }

    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int EmployeeID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "FristName")]
        public string FristName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Position")]
        public string Position { get; set; }
    }

}