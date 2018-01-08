﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Temporary_Prison.Models
{
    public class PrisonerPagedListViewModel
    {
        public int PrisonerId { get; set; }
        [Display(Name = "Birth Date")]
        public string BirthDate { get; set; }
        [Display(Name = "Name")]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
    }
}