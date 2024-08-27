namespace ESC_Assessment.Models
{
    public class Job
    {
        public int ID { get; set; }
        public string Title { get; set; } = "";
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
    }
}
