using WebApplication1.DTOs;
using WebApplication1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            
            _logger.LogWarning("Ошибки валидации при входе: {Errors}", string.Join(", ", errors));
            return BadRequest(new { Errors = errors });
        }
        
        try
        {
            var response = await _authService.Authenticate(loginDto);
            if (response == null)
                return Unauthorized(new { Error = "Неверный email или пароль" });

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при входе пользователя");
            return StatusCode(500, new { Error = "Внутренняя ошибка сервера" });
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            
            _logger.LogWarning("Ошибки валидации при регистрации: {Errors}", string.Join(", ", errors));
            return BadRequest(new { Errors = errors });
        }
        
        try
        {
            var user = await _authService.Register(registerDto);
            return Ok(new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при регистрации пользователя");
            return BadRequest(ex.Message);
        }
    }
}