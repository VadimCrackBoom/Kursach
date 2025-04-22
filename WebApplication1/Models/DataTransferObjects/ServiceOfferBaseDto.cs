using System.ComponentModel.DataAnnotations;

namespace AutoServiceApi.Models.DataTransferObjects;

public class ServiceOfferBaseDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }

    [Required]
    public string Category { get; set; } = "Основные";

    [Range(1, 480)]
    public int DurationMinutes { get; set; } = 60;

    public bool IsActive { get; set; } = true;
}