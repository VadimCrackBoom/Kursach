﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.AutoServiceApi.Models;
using WebApplication1.Data;

namespace AutoServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoservicesController : ControllerBase
    {
        private readonly AutoServiceContext _context;

        public AutoservicesController(AutoServiceContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autoservice>>> GetAutoServices()
            => await _context.Autoservices.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Autoservice>> GetAutoService(int id)
        {
            var autoService = await _context.Autoservices.FindAsync(id);
            return autoService == null ? NotFound() : autoService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutoService(int id, Autoservice autoService)
        {
            if (id != autoService.Id) return BadRequest();
            _context.Entry(autoService).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
