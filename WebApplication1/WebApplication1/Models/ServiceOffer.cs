using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class ServiceOffer
{
    [Key]
    public int ServiceOfferId { get; set; }
    
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Required]
    public int AutoServiceId { get; set; }
    public AutoService AutoService { get; set; }
    
    public ICollection<Appointment> Appointments { get; set; }
}