using WebApplication1.Data;
using WebApplication1.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AutoServicesController : ControllerBase
{
    private readonly AutoServiceContext _context;
    private readonly ILogger<AutoServicesController> _logger;

    public AutoServicesController(AutoServiceContext context, ILogger<AutoServicesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var autoServices = await _context.AutoServices
                .Select(a => new AutoServiceDto
                {
                    AutoServiceId = a.AutoServiceId,
                    Name = a.Name,
                    Address = a.Address,
                    ContactPhone = a.ContactPhone
                }).ToListAsync();

            return Ok(autoServices);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all auto services");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var autoService = await _context.AutoServices.FindAsync(id);
            if (autoService == null) return NotFound();

            return Ok(new AutoServiceDto
            {
                AutoServiceId = autoService.AutoServiceId,
                Name = autoService.Name,
                Address = autoService.Address,
                ContactPhone = autoService.ContactPhone
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting auto service with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] AutoServiceCreateDto createDto)
    {
        try
        {
            var autoService = new Models.AutoService
            {
                Name = createDto.Name,
                Address = createDto.Address,
                ContactPhone = createDto.ContactPhone
            };

            _context.AutoServices.Add(autoService);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = autoService.AutoServiceId }, autoService);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating auto service");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] AutoServiceUpdateDto updateDto)
    {
        try
        {
            var autoService = await _context.AutoServices.FindAsync(id);
            if (autoService == null) return NotFound();

            autoService.Name = updateDto.Name ?? autoService.Name;
            autoService.Address = updateDto.Address ?? autoService.Address;
            autoService.ContactPhone = updateDto.ContactPhone ?? autoService.ContactPhone;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating auto service with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var autoService = await _context.AutoServices.FindAsync(id);
            if (autoService == null) return NotFound();

            _context.AutoServices.Remove(autoService);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting auto service with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }
}