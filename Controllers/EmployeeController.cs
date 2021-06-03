using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

          private readonly Context _db;

        public EmployeeController(Context context)
        {
            _db = context;
        }

        // GET api/Employee
        [HttpGet]
         public async Task<Response<List<Employee>>> GetAll(String? key)
        {
            List<Employee> employees;
            if (key != "" && key != null)
            {
                employees = await _db.employees.Where(x => x.FirstName.ToLower().Contains(key) || x.LastName.ToLower().Contains(key) || x.Email.ToLower().Contains(key) || x.Contact.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                employees = await _db.employees.ToListAsync();
            }
            return new Response<List<Employee>>(true, "Operation Successful", employees);
        }

        // GET api/Employee/5
        [HttpGet("{id}")]
        public async Task<Response<Employee>> GetSingle(long id)
        {
            var Employee = await _db.employees.FirstOrDefaultAsync(x => x.Id == id);
            if (Employee == null)
                return new Response<Employee>(false, "Record not found", null);
            return new Response<Employee>(true, "Operation Successful", Employee);
        }

        // POST api/Employee
       [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee Employee)
        {
            _db.employees.Update(Employee);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Employee.Id }, Employee);
        }

        // PUT api/Employee/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Employee Employee)
        {
            if (id != Employee.Id)
                return BadRequest();
            _db.Entry(Employee).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Employee = await _db.employees.FindAsync(id);

            if (Employee == null)
                return NotFound();

            _db.employees.Remove(Employee);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
