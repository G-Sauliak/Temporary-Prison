using System;

namespace Temporary_Prison.Common.Models
{
    public class RegistDetention
    {
        public int PrisonerID { get; set; }
        public DateTime? DateOfDetention { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public string PlaceofDetention { get; set; }
        public Employee DetainedEmployee { get; set; }
        public Employee DeliveredEmployee { get; set; }
    }
    public class DetentionPagedList
    {
        public int DetentionID { get; set; }
        public DateTime? DateOfDetention { get; set; }
        public string PlaceofDetention { get; set; }
        public DateTime? DateOfRelease { get; set; }
    }
    public class Detention
    {
        public int DetentionID { get; set; }
        public int PrisonerID { get; set; }
        public DateTime? DateOfDetention { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public DateTime? DateOfRelease { get; set; }
        public double AccruedAmount { get; set; }
        public double PaidAmount { get; set; }
        public string PlaceofDetention { get; set; }
        public Employee ReleasedEmployee { get; set; }
        public Employee DetainedEmployee { get; set; }
        public Employee DeliveredEmployee { get; set; }
    }

    public class Prisoner
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

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
    }
}
