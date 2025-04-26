namespace WebApplication1.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; }
    public UserDto User { get; set; }
    
    public DateTime TokenExpiration { get; set; }
}