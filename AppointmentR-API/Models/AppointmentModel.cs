namespace AppointmentR_API.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public UserModel Customer { get; set; }
        public int ServiceId { get; set; }
        public ServiceModel Service { get; set; }
        public int EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
