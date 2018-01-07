using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temporary_Prison.Models
{
    public class ReleaseOfPrisonerViewModel
    {
        [HiddenInput(DisplayValue =false)]
        public int DetentionID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfRelease { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double AccruedAmount { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double PaidAmount { get; set; }
        public EmployeeViewModel ReleasedEmployee { get; set; }
    }
}