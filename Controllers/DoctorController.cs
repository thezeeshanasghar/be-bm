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
    public class DoctorController : ControllerBase
    {

          private readonly Context _db;

        public DoctorController(Context context)
        {
            _db = context;
        }

        // GET api/Doctor
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
        {
            return await _db.doctors.ToListAsync();
        }

        // GET api/Doctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetSingle(long id)
        {
            var Doctor = await _db.doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (Doctor == null)
                return NotFound();

            return Doctor;
        }

        // POST api/Doctor
       [HttpPost]
        public async Task<ActionResult<Doctor>> Post(Doctor Doctor)
        {
            _db.doctors.Update(Doctor);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Doctor.Id }, Doctor);
        }

        // PUT api/Doctor/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Doctor Doctor)
        {
            if (id != Doctor.Id)
                return BadRequest();
            _db.Entry(Doctor).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Doctor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Doctor = await _db.doctors.FindAsync(id);

            if (Doctor == null)
                return NotFound();

            _db.doctors.Remove(Doctor);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
