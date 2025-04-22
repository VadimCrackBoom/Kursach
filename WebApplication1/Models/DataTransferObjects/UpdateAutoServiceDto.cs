using System.ComponentModel.DataAnnotations;

namespace AutoServiceApi.Models.DataTransferObjects;

public class UpdateAutoServiceDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(200)]
    public string Address { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
}