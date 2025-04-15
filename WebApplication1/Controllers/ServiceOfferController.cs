using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.AutoServiceApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ServiceOfferController : ControllerBase
{
    private readonly AutoServiceContext _context;

    public ServiceOfferController(AutoServiceContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceOffer>>> GetServiceOffers()
    {
        return await _context.ServicesOffers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceOffer>> GetServiceOffer(int id)
    {
        // Исправлено: используем FirstOrDefaultAsync вместо FindAsync
        var serviceOffer = await _context.ServicesOffers
            .FirstOrDefaultAsync(s => s.Id == id);

        if (serviceOffer == null)
        {
            return NotFound();
        }

        return serviceOffer;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceOffer>> PostServiceOffer(ServiceOffer serviceOffer)
    {
        serviceOffer.CreatedDate = DateTime.UtcNow;
        serviceOffer.LastUpdated = DateTime.UtcNow;
        
        _context.ServicesOffers.Add(serviceOffer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetServiceOffer), new { id = serviceOffer.Id }, serviceOffer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutServiceOffer(int id, ServiceOffer serviceOffer)
    {
        if (id != serviceOffer.Id)
        {
            return BadRequest();
        }

        serviceOffer.LastUpdated = DateTime.UtcNow;
        _context.Entry(serviceOffer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ServiceOfferExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceOffer(int id)
    {
        var serviceOffer = await _context.ServicesOffers.FindAsync(id);
        if (serviceOffer == null)
        {
            return NotFound();
        }

        _context.ServicesOffers.Remove(serviceOffer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> ServiceOfferExists(int id)
    {
        return await _context.ServicesOffers.AnyAsync(e => e.Id == id);
    }
}