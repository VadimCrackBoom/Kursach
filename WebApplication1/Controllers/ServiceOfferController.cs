using AutoMapper;
using AutoServiceApi.Data;
using AutoServiceApi.Models.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.AutoServiceApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ServiceOfferController : ControllerBase
{
    private readonly AutoServiceContext _context;
    private readonly IMapper _mapper;

    public ServiceOfferController(AutoServiceContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceOfferResponseDto>>> GetServiceOffers()
    {
        var offers = await _context.ServicesOffers.ToListAsync();
        return _mapper.Map<List<ServiceOfferResponseDto>>(offers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceOfferResponseDto>> GetServiceOffer(int id)
    {
        var serviceOffer = await _context.ServicesOffers
            .FirstOrDefaultAsync(s => s.Id == id);

        if (serviceOffer == null)
        {
            return NotFound();
        }

        return _mapper.Map<ServiceOfferResponseDto>(serviceOffer);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceOfferResponseDto>> PostServiceOffer(CreateServiceOfferDto createDto)
    {
        var serviceOffer = _mapper.Map<ServiceOffer>(createDto);
        serviceOffer.CreatedDate = DateTime.UtcNow;
        serviceOffer.LastUpdated = DateTime.UtcNow;
        
        _context.ServicesOffers.Add(serviceOffer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetServiceOffer), 
            new { id = serviceOffer.Id }, 
            _mapper.Map<ServiceOfferResponseDto>(serviceOffer));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutServiceOffer(int id, UpdateServiceOfferDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest();
        }

        var serviceOffer = await _context.ServicesOffers.FindAsync(id);
        if (serviceOffer == null)
        {
            return NotFound();
        }

        _mapper.Map(updateDto, serviceOffer);
        
        if (updateDto.UpdateTimestamp)
        {
            serviceOffer.LastUpdated = DateTime.UtcNow;
        }

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

