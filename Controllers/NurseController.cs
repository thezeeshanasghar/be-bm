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
    public class NurseController : ControllerBase
    {

          private readonly Context _db;

        public NurseController(Context context)
        {
            _db = context;
        }

        // GET api/Nurse
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Nurse>>> GetAll(String? key)
        {
            if (key != "" && key != null)
            {
                return await _db.nurses.Include(x => x.Employee).Where(x => x.Employee.FirstName.ToLower().Contains(key) || x.Employee.LastName.ToLower().Contains(key) || x.Employee.Email.ToLower().Contains(key) || x.Employee.Contact.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                return await _db.nurses.Include(x => x.Employee).ToListAsync();
            }
        }

        // GET api/Nurse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nurse>> GetSingle(long id)
        {
            var Nurse = await _db.nurses.FirstOrDefaultAsync(x => x.Id == id);
            if (Nurse == null)
                return NotFound();

            return Nurse;
        }

        // POST api/Nurse
       [HttpPost]
        public async Task<ActionResult<Nurse>> Post(Nurse Nurse)
        {
            _db.nurses.Update(Nurse);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Nurse.Id }, Nurse);
        }

        // PUT api/Nurse/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Nurse Nurse)
        {
            if (id != Nurse.Id)
                return BadRequest();
            _db.Entry(Nurse).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Nurse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Nurse = await _db.nurses.FindAsync(id);

            if (Nurse == null)
                return NotFound();

            _db.nurses.Remove(Nurse);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
