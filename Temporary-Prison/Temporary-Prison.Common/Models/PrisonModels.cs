using System;
using Temporary_Prison.Common.Entities;

namespace Temporary_Prison.Common.Models
{
    public class RegistDetention
    {
        public int PrisonerID { get; set; }
        public DateTime DateOfDetention { get; set; }
        public DateTime DateOfArrival { get; set; }
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
}
