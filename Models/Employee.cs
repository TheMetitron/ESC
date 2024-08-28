namespace ESC_Assessment.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string HireDate { get; set; }
        public int JobID { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentID { get; set; }
        public int ManagerID { get; set; }
      
    }
}
