using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temporary_Prison.Models
{
    public class DetailsOfDetentionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int DetentionID { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int PrisonerID { get; set; }
        [Display(Name = "Date of Detention")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? DateOfDetention { get; set; }
        [Display(Name = "Date of arrival")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? DateOfArrival { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of release")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? DateOfRelease { get; set; }
        [Display(Name = "Accrued Amount")]
        public double AccruedAmount { get; set; }
        [Display(Name = "Paid Amount")]
        public double PaidAmount { get; set; }
        [Display(Name = "Place of Detention")]
        public string PlaceofDetention { get; set; }
        public EmployeeViewModel ReleasedEmployee { get; set; }
        public EmployeeViewModel DetainedEmployee { get; set; }
        public EmployeeViewModel DeliveredEmployee { get; set; }
    }
}