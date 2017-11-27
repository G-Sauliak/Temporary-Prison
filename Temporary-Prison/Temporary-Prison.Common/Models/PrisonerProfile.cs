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
        public DateTime? BirthDate { get; set; }
        public string AdditionalInformation { get; set; }
        public string Avatar { get; set; }
        public PlaceOfResidence placeOfResidence { get; set; }
    }

    public class PlaceOfResidence
    {
        public string County { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
