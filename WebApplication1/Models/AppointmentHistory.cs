using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class AppointmentHistory
{
    [Key]
    public int AppointmentHistoryId { get; set; }
    
    [Required]
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }

    public DateTime RecordDate { get; set; } = DateTime.UtcNow;
}