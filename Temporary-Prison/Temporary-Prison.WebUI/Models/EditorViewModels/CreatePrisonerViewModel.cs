﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Temporary_Prison.Business.Attributes;
using Temporary_Prison.Enums;

namespace Temporary_Prison.Models
{
    public class CreateOrUpdatePrisonerViewModel
    {
        public CreateOrUpdatePrisonerViewModel() { }

        [HiddenInput]
        public int PrisonerID { get; set; }

        [Display(Name = "Photo")]
        [HiddenInput(DisplayValue = false)]
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
        [EnumDataType(typeof(RelationshipStatus))]
        public RelationshipStatus RelationshipStatus { get; set; }

        [Required]
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