using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models;

public class User
{
    [Key]
    public int UserId { get; set; }
    
    [Required]
    public string FullName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public ICollection<Appointment> Appointments { get; set; }
}