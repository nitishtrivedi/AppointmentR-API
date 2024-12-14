
using AppointmentR_API.Data;
using AppointmentR_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace AppointmentR_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EmployeeController(AppDBContext context)

        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployees()
        {
            return await _context.Employees
                .Include(e => e.Availability) 
                .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Availability)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null) 
                return NotFound();
            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> AddEmployee(EmployeeModel employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeModel>> EditEmployee(EmployeeModel employee, int id)
        {
            if (id != employee.Id) return BadRequest();
            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeModel>> DeleteEmployee(int id)
        {
           
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound(); 
            
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(); 
        }
    }
}

