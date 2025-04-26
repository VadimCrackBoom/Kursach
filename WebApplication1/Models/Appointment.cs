using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Appointment
{
    [Key]
    public int AppointmentId { get; set; }
    
    [Required]
    public DateTime AppointmentDateTime { get; set; }
    
    [Required]
    public string Status { get; set; }
    
    [Required]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Required]
    public int ServiceOfferId { get; set; }
    public ServiceOffer ServiceOffer { get; set; }
    
    public AppointmentHistory AppointmentHistory { get; set; }
}