namespace AppointmentR_API.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public ICollection<EmployeeAvailability> Availability {  get; set; }
    }
}
