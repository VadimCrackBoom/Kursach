using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AutoServiceContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(AutoServiceContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var users = await _context.Users
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email
                }).ToListAsync();

            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка получения всех пользователей");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (id != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

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
            _logger.LogError(ex, $"Ошибка получения пользователя по ID {id}");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto updateDto)
    {
        try
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (id != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.FullName = updateDto.FullName ?? user.FullName;
            user.PhoneNumber = updateDto.PhoneNumber ?? user.PhoneNumber;
            if (!string.IsNullOrEmpty(updateDto.Password))
                user.Password = BCrypt.Net.BCrypt.HashPassword(updateDto.Password);

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка обновления пользователя по ID {id}");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }
}