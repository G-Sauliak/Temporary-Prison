﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Temporary_Prison.Models
{
    public class PrisonerPagedListViewModel
    {
        [Display(Name = "Avatar")]
        public string Avatar { get; set; }
        public int PrisonerId { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime? BirthDate { get; set; }
        [Display(Name = "Name")]
        public string FirstName { get; set; }
        [Display(Name = "Name")]
        public string Surname { get; set; }
        [Display(Name = "Name")]
        public string LastName { get; set; }
    }
}