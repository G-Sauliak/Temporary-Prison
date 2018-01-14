using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temporary_Prison.Models
{
    public class DetentionPagedListViewModel
    {
        [HiddenInput(DisplayValue =false)]
        public int DetentionID { get; set; }
        [Display(Name = "Date Of Detention")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? DateOfDetention { get; set; }
        [Display(Name = "Place Of Detention")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public string PlaceofDetention { get; set; }
        [Display(Name = "Date Of Release")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? DateOfRelease { get; set; }
    }
}