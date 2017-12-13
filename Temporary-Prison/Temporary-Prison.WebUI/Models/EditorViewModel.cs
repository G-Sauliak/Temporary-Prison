using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Temporary_Prison.Business.Attributes;

namespace Temporary_Prison.Models
{
    public class AddPrisonerViewModel
    {

        public AddPrisonerViewModel()
        { 
        }
     
        public int PrisonerID { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [PhoneNumber(ErrorMessage = "Phone format is not valid.")]
        [Display(Name = "Phone Number")]
        public IReadOnlyList<string> PhoneNumbers { get; set; }

        [Required]
        [Display(Name = "Relationship Status")]
        public string RelationshipStatus { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "only alphabetic characters are allowed.")]
        [Display(Name = "Place of work")]
        public string PlaceOfWork { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date ")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 2)]
        [Display(Name = "Additional Information")]
        public string AdditionalInformation { get; set; }

    }
}