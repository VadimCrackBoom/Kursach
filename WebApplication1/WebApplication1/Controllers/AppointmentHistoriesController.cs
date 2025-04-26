using WebApplication1.Data;
using WebApplication1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class AppointmentHistoriesController : ControllerBase
{
    private readonly AutoServiceContext _context;
    private readonly ILogger<AppointmentHistoriesController> _logger;

    public AppointmentHistoriesController(AutoServiceContext context, ILogger<AppointmentHistoriesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? appointmentId = null)
    {
        try
        {
            var query = _context.AppointmentHistories
                .Include(h => h.Appointment)
                .ThenInclude(a => a.User)
                .Include(h => h.Appointment)
                .ThenInclude(a => a.ServiceOffer)
                .AsQueryable();

            if (appointmentId.HasValue)
                query = query.Where(h => h.AppointmentId == appointmentId.Value);

            var histories = await query
                .Select(h => new AppointmentHistoryDto
                {
                    AppointmentHistoryId = h.AppointmentHistoryId,
                    AppointmentId = h.AppointmentId,
                    RecordDate = h.RecordDate
                }).ToListAsync();

            return Ok(histories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all appointment histories");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var history = await _context.AppointmentHistories
                .Include(h => h.Appointment)
                .ThenInclude(a => a.User)
                .Include(h => h.Appointment)
                .ThenInclude(a => a.ServiceOffer)
                .FirstOrDefaultAsync(h => h.AppointmentHistoryId == id);

            if (history == null) return NotFound();

            return Ok(new AppointmentHistoryDto
            {
                AppointmentHistoryId = history.AppointmentHistoryId,
                AppointmentId = history.AppointmentId,
                RecordDate = history.RecordDate
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting appointment history with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }
}