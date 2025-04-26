using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class AutoServiceCreateDto
{
    [Required] public string Name { get; set; }
    [Required] public string Address { get; set; }
    [Required] public string ContactPhone { get; set; }
}