using AppointmentR_API.Data;
using AppointmentR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentR_API.Controllers
{
    public class EmployeeAvailabilityController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EmployeeAvailabilityController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeAvailability>>> GetEmployeeAvailability()
        {
            var availability = await _context.EmployeeAvailability
                .Include(e => e.Employee)  
                .ToListAsync();

            return Ok(availability);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeAvailability>>> GetEmployeeAvailabilityByEmployee(int id)
        {
            var availability = await _context.EmployeeAvailability
                .Where(e => e.Id == id)
                .Include(ea => ea.Employee)  
                .ToListAsync();

            if (availability == null) {
                return NotFound();
            }
            return Ok(availability);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeAvailability>> AddEmployeeAvailability(EmployeeAvailability availability)
        {
            
            if (availability.StartTime >= availability.EndTime)
            {
                return BadRequest("Give Start before the end time.");
            }

            _context.EmployeeAvailability.Add(availability); 
            await _context.SaveChangesAsync();
            return Ok(availability);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployeeAvailability(EmployeeAvailability availability , int id)
        {
            if (id != availability.Id)
            {
                return BadRequest("Availability ID mismatch.");
            }

            var existingAvailability = await _context.EmployeeAvailability.FindAsync(id);
            if (existingAvailability == null)
            {
                return NotFound();
            }

            if (availability.StartTime >= availability.EndTime)
            {
                return BadRequest("Start time must be before the end time.");
            }
            _context.Entry(existingAvailability).CurrentValues.SetValues(availability);
            try 
            { 
                await _context.SaveChangesAsync();
            } 
            catch (DbUpdateConcurrencyException)
            { 
                if (!_context.EmployeeAvailability
                    .Any(e => e.Id == id)) 
                {
                    return NotFound();
                } 
                throw; 
            }
            return Ok(availability);
            
        }


    }
}
