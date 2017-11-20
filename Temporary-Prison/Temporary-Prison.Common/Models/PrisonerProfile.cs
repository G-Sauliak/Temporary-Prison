using System;

namespace Temporary_Prison.Common.Models
{
    public class PrisonerProfile
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }
        public string RelationshipStatus { get; set; }
        public string PlaceOfWork { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Avatar { get; set; }
        public string PlaceOfResidence { get; set; }
    }
}
