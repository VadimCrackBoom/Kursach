using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.AutoServiceApi.Models;
using WebApplication1.Data;

namespace AutoServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentHistoriesController : ControllerBase
    {
        private readonly AutoServiceContext _context;

        public AppointmentHistoriesController(AutoServiceContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentHistory>>> GetHistories()
            => await _context.AppointmentHistories
                .Include(h => h.Appointment)
                .ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentHistory>> GetHistory(int id)
        {
            var history = await _context.AppointmentHistories
                .Include(h => h.Appointment)
                .FirstOrDefaultAsync(h => h.Id == id);
            return history == null ? NotFound() : history;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentHistory>> PostHistory(AppointmentHistory history)
        {
            _context.AppointmentHistories.Add(history);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHistory), new { id = history.Id }, history);
        }
    }
}
