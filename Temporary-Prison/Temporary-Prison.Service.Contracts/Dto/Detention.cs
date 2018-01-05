using System;
using System.Runtime.Serialization;
using Temporary_Prison.Common.Entities;

namespace Temporary_Prison.Service.Contracts.Dto
{
    [DataContract]
    public class DetentionDto
    {
        [DataMember]
        public int DetentionID { get; set; }
        [DataMember]
        public int PrisonerID { get; set; }
        [DataMember]
        public DateTime? DateOfDetention { get; set; }
        [DataMember]
        public DateTime? DateOfArrival { get; set; }
        [DataMember]
        public DateTime? DateOfRelease { get; set; }
        [DataMember]
        public double? AccruedAmount { get; set; }
        [DataMember]
        public double? PaidAmount { get; set; }
        [DataMember]
        public string PlaceofDetention { get; set; }
        [DataMember]
        public EmployeeDto DeliveredEmployee { get; set; }
        [DataMember]
        public EmployeeDto DetainedEmployee { get; set; }
        [DataMember]
        public EmployeeDto ReleasedEmployee { get; set; }
    }

    [DataContract]
    public class DetentionPagedListDto
    {
        [DataMember]
        public int DetentionID { get; set; }
        [DataMember]
        public DateTime? DateOfDetention { get; set; }
        [DataMember]
        public string PlaceofDetention { get; set; }
        [DataMember]
        public DateTime? DateOfRelease { get; set; }
    }
    public class ReleaseOfPrisonerDto
    {
        public int DetentionID { get; set; }
        public DateTime DateOfRelease { get; set; }
        public double AccruedAmount { get; set; }
        public double PaidAmount { get; set; }
    }
    public class RegistrationOfDetentionDto
    {
        public int PrisonerID { get; set; }
        public DateTime DateOfDetention { get; set; }
        public DateTime DateOfArrival { get; set; }
        public string PlaceofDetention { get; set; }
        public EmployeeDto DetainedEmployee { get; set; }
        public EmployeeDto DeliveredEmployee { get; set; }
    }

    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
    }
}
