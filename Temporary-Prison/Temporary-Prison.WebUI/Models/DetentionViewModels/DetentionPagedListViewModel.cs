using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temporary_Prison.Models
{
    public class DetentionPagedListViewModel
    {
        [HiddenInput(DisplayValue =false)]
        public int DetentionID { get; set; }
        [Display(Name = "Date Of Detention")]
        public string DateOfDetention { get; set; }
        [Display(Name = "Place Of Detention")]
        public string PlaceofDetention { get; set; }
        [Display(Name = "Date Of Release")]
        public string DateOfRelease { get; set; }
    }
}