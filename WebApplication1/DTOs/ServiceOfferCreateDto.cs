using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class ServiceOfferCreateDto
{
    [Required] public string Name { get; set; }
    public string? Description { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public int AutoServiceId { get; set; }
}