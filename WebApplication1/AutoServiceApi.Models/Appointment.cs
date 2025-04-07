namespace WebApplication1.AutoServiceApi.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public string Status { get; set; } = string.Empty;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public int ServiceId { get; set; }

        public Service Service { get; set; } = null!;

        public AppointmentHistory? History { get; set; }
    }
}
