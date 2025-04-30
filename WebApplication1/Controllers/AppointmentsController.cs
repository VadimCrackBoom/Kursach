using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AutoServiceContext _context;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(AutoServiceContext context, ILogger<AppointmentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? userId = null)
    {
        try
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");

            var query = _context.Appointments
                .Include(a => a.ServiceOffer)
                .Include(a => a.User)
                .AsQueryable();

            // Regular users can only see their own appointments
            if (!isAdmin)
                query = query.Where(a => a.UserId == currentUserId);
            else if (userId.HasValue)
                query = query.Where(a => a.UserId == userId.Value);

            var appointments = await query
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    AppointmentDateTime = a.AppointmentDateTime,
                    Status = a.Status,
                    UserId = a.UserId,
                    ServiceOfferId = a.ServiceOfferId
                }).ToListAsync();

            return Ok(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка получения всех записей");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");

            var appointment = await _context.Appointments
                .Include(a => a.ServiceOffer)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null) return NotFound();

            // Check if current user owns the appointment or is admin
            if (appointment.UserId != currentUserId && !isAdmin)
                return Forbid();

            return Ok(new AppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Status = appointment.Status,
                UserId = appointment.UserId,
                ServiceOfferId = appointment.ServiceOfferId,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка получения записи по ID {id}");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto createDto)
    {
        try
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var serviceOffer = await _context.ServiceOffers.FindAsync(createDto.ServiceOfferId);
            if (serviceOffer == null)
                return BadRequest(new { message = "Такого ID автосервиса не существует" });

            var appointment = new Appointment
            {
                AppointmentDateTime = createDto.AppointmentDateTime,
                Status = "Pending",
                UserId = currentUserId,
                ServiceOfferId = createDto.ServiceOfferId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Create history record
            var history = new AppointmentHistory
            {
                AppointmentId = appointment.AppointmentId
            };
            _context.AppointmentHistories.Add(history);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = appointment.AppointmentId }, appointment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка создания записи");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    [HttpPut("{id}/status")]
    [Authorize]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateAppointmentStatusDto updateDto)
    {
        try
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            appointment.Status = updateDto.Status;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка обновления записи с ID {id}");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var isAdmin = User.IsInRole("Admin");

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            
            if (appointment.UserId != currentUserId && !isAdmin)
                return Forbid();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting appointment with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }
}