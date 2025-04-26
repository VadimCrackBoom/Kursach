using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class AutoService
{
    [Key]
    public int AutoServiceId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string ContactPhone { get; set; }
    
    public ICollection<ServiceOffer> ServiceOffers { get; set; }
}