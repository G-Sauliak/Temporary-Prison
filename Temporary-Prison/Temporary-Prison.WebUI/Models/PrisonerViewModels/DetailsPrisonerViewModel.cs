using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temporary_Prison.Models
{
    public class DetailsPrisonerViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PrisonerId { get; set; }
        [Display(Name = "Relationship status")]
        public string RelationshipStatus { get; set; }
        [Display(Name = "Phone numbers")]
        public string[] PhoneNumbers { get; set; }
        [Display(Name = "Place of work")]
        public string PlaceOfWork { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Photo")]
        public string Photo { get; set; }
        [Display(Name = "Additional information")]
        public string AdditionalInformation { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        public IEnumerable<DetentionPagedListViewModel> DetentionPagedList;
    }
}