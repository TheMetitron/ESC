namespace ESC_Assessment.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string StreetAddress { get; set; } = "";
        public string City { get; set; } = "";
        public string StateProvince { get; set; } = "";
        public string PostalCode { get; set; } = "";
    }
}
