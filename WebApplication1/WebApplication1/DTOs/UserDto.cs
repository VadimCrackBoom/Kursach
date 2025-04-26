using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}

public class UserRegisterDto
{
    [Required]
    public string FullName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}