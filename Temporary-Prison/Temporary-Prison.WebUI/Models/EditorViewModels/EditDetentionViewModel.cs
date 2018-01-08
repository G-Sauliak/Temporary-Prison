using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temporary_Prison.Models
{
    public class EditFullDetentionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int DetentionID { get; set; }
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
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Release")]
        public DateTime DateOfRelease { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Accured Amount")]
        public decimal AccruedAmount { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Paid Amount")]
        public decimal PaidAmount { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Place Of Detention")]
        public string PlaceofDetention { get; set; }
        public EmployeeViewModel ReleasedEmployee { get; set; }
        public EmployeeViewModel DetainedEmployee { get; set; }
        public EmployeeViewModel DeliveredEmployee { get; set; }
    }
    public class EditDetentionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int DetentionID { get; set; }
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
}