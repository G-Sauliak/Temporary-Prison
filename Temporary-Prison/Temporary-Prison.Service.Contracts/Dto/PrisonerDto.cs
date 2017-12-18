using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Temporary_Prison.Common.Models;

namespace Temporary_Prison.Service.Contracts.Dto
{
    [DataContract]
    public class PrisonerDto
    {
        [DataMember]
        public int PrisonerId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string RelationshipStatus { get; set; }
        [DataMember]
        public string PlaceOfWork { get; set; }
        [DataMember]
        public string[] PhoneNumbers { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string AdditionalInformation { get; set; }
        [DataMember]
        public string Photo { get; set; }

    }
}
