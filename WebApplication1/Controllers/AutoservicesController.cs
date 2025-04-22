using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoServiceApi.Data;
using AutoServiceApi.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.AutoServiceApi.Models;

namespace AutoServiceApi.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class AutoServicesController : ControllerBase
{
    private readonly AutoServiceContext _context;

    public AutoServicesController(AutoServiceContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AutoServiceResponseDto>>> GetAutoServices()
    {
        return await _context.AutoServices
            .Select(a => new AutoServiceResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                Address = a.Address,
                Phone = a.Phone,
                WorkingHours = $"{a.OpeningTime:hh\\:mm} - {a.ClosingTime:hh\\:mm}"
            })
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AutoServiceResponseDto>> GetAutoService(int id)
    {
        var autoService = await _context.AutoServices.FindAsync(id);
        
        if (autoService == null) 
            return NotFound();
            
        return new AutoServiceResponseDto
        {
            Id = autoService.Id,
            Name = autoService.Name,
            Address = autoService.Address,
            Phone = autoService.Phone,
            WorkingHours = $"{autoService.OpeningTime:hh\\:mm} - {autoService.ClosingTime:hh\\:mm}"
        };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAutoService(int id, AutoServiceDto dto)
    {
        if (id != dto.Id) 
            return BadRequest();
            
        var autoService = await _context.AutoServices.FindAsync(id);
        if (autoService == null)
            return NotFound();
            
        autoService.Name = dto.Name;
        autoService.Address = dto.Address;
        autoService.Phone = dto.Phone;
        autoService.OpeningTime = dto.OpeningTime;
        autoService.ClosingTime = dto.ClosingTime;
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}
}
