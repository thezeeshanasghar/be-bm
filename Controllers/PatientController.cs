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
    public class PatientController : ControllerBase
    {

          private readonly Context _db;

        public PatientController(Context context)
        {
            _db = context;
        }

        // GET api/Patient
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Patient>>> GetAll(string? key)
        {
            if (key != "" && key != null)
            {
                return await _db.patients.Where(x => x.Name.ToLower().Contains(key) || x.Email.ToLower().Contains(key) || x.Contact.ToLower().Contains(key) ||  x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                return await _db.patients.ToListAsync();
            }
        }

        // GET api/Patient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetSingle(long id)
        {
            var Patient = await _db.patients.FirstOrDefaultAsync(x => x.Id == id);
            if (Patient == null)
                return NotFound();

            return Patient;
        }

        // POST api/Patient
       [HttpPost]
        public async Task<ActionResult<Patient>> Post(Patient Patient)
        {
            _db.patients.Update(Patient);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Patient.Id }, Patient);
        }

        // PUT api/Patient/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Patient Patient)
        {
            if (id != Patient.Id)
                return BadRequest();
            _db.Entry(Patient).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Patient = await _db.patients.FindAsync(id);

            if (Patient == null)
                return NotFound();

            _db.patients.Remove(Patient);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
