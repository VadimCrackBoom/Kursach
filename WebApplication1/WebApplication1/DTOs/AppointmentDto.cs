using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
    public int ServiceOfferId { get; set; }
}

public class CreateAppointmentDto
{
    [Required]
    public DateTime AppointmentDateTime { get; set; }
    
    [Required]
    public int ServiceOfferId { get; set; }
}