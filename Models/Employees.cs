namespace ESC_Assessment.Models
{
    public class Employees
    {
        public Employee Employee { get; set; }
        public Dependent Dependent { get; set; }
        public Job Job { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }
    }
}
