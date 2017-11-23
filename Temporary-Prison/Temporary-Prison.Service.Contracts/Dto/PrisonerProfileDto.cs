using System;
using System.Runtime.Serialization;

namespace Temporary_Prison.Service.Contracts.Dto
{
    [DataContract]
    public class PrisonerProfileDto
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Patronymic { get; set; }
        [DataMember]
        public string LastName { get; set; }
        public string RelationshipStatus { get; set; }
        public string PlaceOfWork { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Avatar { get; set; }
        public string PlaceOfResidence { get; set; }
    }
}
