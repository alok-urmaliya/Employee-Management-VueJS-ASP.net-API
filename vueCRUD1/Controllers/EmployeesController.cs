using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vueCRUD1.Data;
using vueCRUD1.Model;

namespace vueCRUD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly vueCRUD1Context _context;

        public EmployeesController(vueCRUD1Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }

            var employees = _context.Employee.Where(x => x.IsActive == true || x.IsActive == null).ToList();
            return employees;
            //to get inactive employees also.
            //return await _context.Employee.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employee == null)
          {
              return NotFound();
          }
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            int err_count = 0;
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (employee.FirstName.Any(char.IsDigit))
            {
                err_count++;
                ModelState.AddModelError("FirstName", "First Name can only contain letters");
            }
            if (employee.LastName.Any(char.IsDigit))
            {
                err_count++;
                ModelState.AddModelError("LastName", "Last Name can only contain letters");
            }
            if (employee.City.Any(char.IsDigit))
            {
                err_count++;
                ModelState.AddModelError("City", "City can only contain letters");
            }
            if (err_count == 0)
            {
                _context.Entry(employee).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            int err_count = 0;
            if (_context.Employee == null)
            {
                return Problem("Entity set 'vueCRUDContext.Employee'  is null.");
            }
            if (employee.FirstName.Any(char.IsDigit))
            {
                err_count++;
                ModelState.AddModelError("FirstName", "First Name can only contain letters");
            }
            if (employee.LastName.Any(char.IsDigit))
            {
                err_count++;
                ModelState.AddModelError("LastName", "Last Name can only contain letters");
            }
            if (employee.City.Any(char.IsDigit))
            {
                err_count++;
                ModelState.AddModelError("City", "City can only contain letters");
            }
            if (err_count == 0)
            {
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
            err_count = 0;
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            //Soft Delete
            employee.IsActive = false;
            _context.Employee.Attach(employee).Property(x => x.IsActive).IsModified = true;
            await _context.SaveChangesAsync();
            return NoContent();

            //Hard Delete
            //_context.Employee.Remove(employee);
            //await _context.SaveChangesAsync();
            //return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
