using System;

namespace Temporary_Prison.Models.PrisonerViewModels
{
    public class DetailsPrisonerViewModel
    {
        public int PrisonerId { get; set; }
        public string RelationshipStatus { get; set; }
        public string[] PhoneNumbers { get; set; }
        public string PlaceOfWork { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public string AdditionalInformation { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}