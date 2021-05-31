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
         public async Task<ActionResult<IEnumerable<Doctor>>> GetAll(string? key)
        {
            if (key != "" && key != null)
            {
                return await _db.doctors.Include(x => x.employee).ThenInclude(x => x.Qualifications).Where(x => x.employee.FirstName.ToLower().Contains(key) || x.employee.LastName.ToLower().Contains(key) || x.employee.Email.ToLower().Contains(key) || x.employee.Contact.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                return await _db.doctors.Include(x => x.employee).ThenInclude(x => x.Qualifications).ToListAsync();
            }
        }

        // GET api/Doctor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetSingle(long id)
        {
            var Doctor = await _db.doctors.Include(x=>x.employee).ThenInclude(x=>x.Qualifications).FirstOrDefaultAsync(x => x.Id == id);
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
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (id != Doctor.Id)
                    return BadRequest();

                _db.Entry(Doctor).State = EntityState.Modified;

                _db.Entry(Doctor.employee).State = EntityState.Modified;
                foreach (Qualifications qualification in Doctor.employee.Qualifications)
                {
                  var resp=  _db.qualifications.Where(x => x.Id == qualification.Id).FirstOrDefault();
                    if (resp != null)
                    {
                        _db.Entry(qualification).State = EntityState.Modified;
                    }
                    else {
                        qualification.Id =0;
                        _db.qualifications.Update(qualification);
                    }
                  
                    await _db.SaveChangesAsync();
                }
                await _db.SaveChangesAsync();
                transaction.Commit();

            }
            catch (Exception ex) {
                transaction.Rollback();
            }
           
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
