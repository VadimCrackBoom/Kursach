using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class UpdateAppointmentStatusDto
{
    [Required] public string Status { get; set; }
}