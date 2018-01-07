using System;

namespace Temporary_Prison.Common.Entities
{
    public class Detention
    {
        public int DetentionID { get; set; }
        public int PrisonerID { get; set; }
        public DateTime? DateOfDetention { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public DateTime? DateOfRelease { get; set; }
        public decimal AccruedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string PlaceofDetention { get; set; }
        public int ReleaseProceduresID { get; set; }
        public int DetentionProceduresID { get; set; }
        public int DeliveredProceduresID { get; set; }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
    }

    public class RegistrationOfDetention
    {
        public int PrisonerID { get; set; }
        public DateTime DateOfDetention { get; set; }
        public DateTime DateOfArrival { get; set; }
        public string PlaceofDetention { get; set; }
        public int DetentionProceduresID { get; set; }
        public int DeliveredProceduresID { get; set; }
    }


    public class ReleaseOfPrisoner
    {
        public int DetentionID { get; set; }
        public DateTime DateOfRelease { get; set; }
        public decimal AccruedAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public int ReleaseProceduresID { get; set; }
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

    public class Phone
    {
        public int PrisonerId { get; set; }
        public string PhoneNumber { get; set; }
    }

}
