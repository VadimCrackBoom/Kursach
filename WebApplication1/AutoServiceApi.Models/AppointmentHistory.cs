namespace WebApplication1.AutoServiceApi.Models
{
    public class AppointmentHistory
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; } = null!; // Навигационное свойство

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
