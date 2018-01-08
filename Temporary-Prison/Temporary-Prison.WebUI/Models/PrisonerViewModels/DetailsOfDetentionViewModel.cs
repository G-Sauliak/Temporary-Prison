namespace Temporary_Prison.Models
{
    public class DetailsOfDetentionViewModel
    {
        public int DetentionID { get; set; }
        public int PrisonerID { get; set; }
        public string DateOfDetention { get; set; }
        public string DateOfArrival { get; set; }
        public string DateOfRelease { get; set; }
        public double AccruedAmount { get; set; }
        public double PaidAmount { get; set; }
        public string PlaceofDetention { get; set; }
        public EmployeeViewModel ReleasedEmployee { get; set; }
        public EmployeeViewModel DetainedEmployee { get; set; }
        public EmployeeViewModel DeliveredEmployee { get; set; }
    }
}