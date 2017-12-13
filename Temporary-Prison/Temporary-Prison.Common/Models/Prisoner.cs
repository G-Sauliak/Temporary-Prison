using System;
using System.Collections.Generic;

namespace Temporary_Prison.Common.Models
{
    public class Prisoner
    {
        public int PrisonerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public IReadOnlyList<string> PhoneNumbers { get; set; }
        public string RelationshipStatus { get; set; }
        public string PlaceOfWork { get; set; }
        public DateTime BirthDate { get; set; }
        public string AdditionalInformation { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }
    }

    public class ListOfDetentions
    {
        public DateTime DateOfDetention { get; set; }
        public DateTime DateOfArrival { get; set; }
        public DateTime DateOfRelease { get; set; }
        public double AccruedAmount { get; set; }
        public double PaidAmount { get; set; }
        public string PlaceofDetention { get; set; }
    }
}
