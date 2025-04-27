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
public class ServiceOffersController : ControllerBase
{
    private readonly AutoServiceContext _context;
    private readonly ILogger<ServiceOffersController> _logger;

    public ServiceOffersController(AutoServiceContext context, ILogger<ServiceOffersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll([FromQuery] int? autoServiceId = null)
    {
        try
        {
            var query = _context.ServiceOffers.AsQueryable();

            if (autoServiceId.HasValue)
                query = query.Where(s => s.AutoServiceId == autoServiceId.Value);

            var serviceOffers = await query
                .Select(s => new ServiceOfferDto
                {
                    ServiceOfferId = s.ServiceOfferId,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    AutoServiceId = s.AutoServiceId
                }).ToListAsync();

            return Ok(serviceOffers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all service offers");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var serviceOffer = await _context.ServiceOffers
                .Include(s => s.AutoService)
                .FirstOrDefaultAsync(s => s.ServiceOfferId == id);

            if (serviceOffer == null) return NotFound();

            return Ok(new ServiceOfferDto
            {
                ServiceOfferId = serviceOffer.ServiceOfferId,
                Name = serviceOffer.Name,
                Description = serviceOffer.Description,
                Price = serviceOffer.Price,
                AutoServiceId = serviceOffer.AutoServiceId,
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting service offer with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] ServiceOfferCreateDto createDto)
    {
        try
        {
            var serviceOffer = new ServiceOffer
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                AutoServiceId = createDto.AutoServiceId
            };

            _context.ServiceOffers.Add(serviceOffer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = serviceOffer.ServiceOfferId }, serviceOffer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating service offer");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] ServiceOfferUpdateDto updateDto)
    {
        try
        {
            var serviceOffer = await _context.ServiceOffers.FindAsync(id);
            if (serviceOffer == null) return NotFound();

            serviceOffer.Name = updateDto.Name ?? serviceOffer.Name;
            serviceOffer.Description = updateDto.Description ?? serviceOffer.Description;
            if (updateDto.Price.HasValue) serviceOffer.Price = updateDto.Price.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating service offer with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var serviceOffer = await _context.ServiceOffers.FindAsync(id);
            if (serviceOffer == null) return NotFound();

            _context.ServiceOffers.Remove(serviceOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting service offer with ID {id}");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }
}