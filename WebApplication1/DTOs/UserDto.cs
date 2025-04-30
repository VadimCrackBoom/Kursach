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
    [Required(ErrorMessage = "Email обязателен для заполнения")]
    [EmailAddress(ErrorMessage = "Некорректный формат Email")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Email должен быть от 5 до 50 символов")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов")]
    public string Password { get; set; }
}

public class UserRegisterDto
{
    [Required(ErrorMessage = "Имя обязательно для заполнения")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 10 символов")]
    public string FullName { get; set; }
    
    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "Телефон обязателен для заполнения")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    [StringLength(15, MinimumLength = 6, ErrorMessage = "Телефон должен быть от 6 до 15 символов")]
    public string PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Email обязателен для заполнения")]
    [EmailAddress(ErrorMessage = "Некорректный формат Email")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Email должен быть от 5 до 50 символов")]
    public string Email { get; set; }
}