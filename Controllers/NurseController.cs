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
          public async Task<Response<List<Nurse>>> GetAll(String? key)
        {
            List<Nurse> nurses;
            if (key != "" && key != null)
            {
                nurses = await _db.nurses.Include(x => x.Employee).ThenInclude(x=>x.Qualifications).Where(x => x.Employee.FirstName.ToLower().Contains(key) || x.Employee.LastName.ToLower().Contains(key) || x.Employee.Email.ToLower().Contains(key) || x.Employee.Contact.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                nurses = await _db.nurses.Include(x => x.Employee).ThenInclude(x=>x.Qualifications).ToListAsync();
            }
             return new Response<List<Nurse>>(true, "Successfully", nurses);
        }

        // GET api/Nurse/5
        [HttpGet("{id}")]
         public async Task<Response<Nurse>> GetSingle(long id)
        {
            var Nurse = await _db.nurses.Include(x=>x.Employee).ThenInclude(x=>x.Qualifications).FirstOrDefaultAsync(x => x.Id == id);
            if (Nurse == null)
                return new Response<Nurse>(false, "Record not found", null);
            return new Response<Nurse>(true, "operation succcessful", Nurse);
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
            using var transaction = _db.Database.BeginTransaction();
            try { 
             if (id != Nurse.Id)
                return BadRequest();

            _db.Entry(Nurse).State = EntityState.Modified;

            _db.Entry(Nurse.Employee).State = EntityState.Modified;
            foreach (Qualifications qualification in Nurse.Employee.Qualifications)
            {
                var resp = _db.qualifications.Where(x => x.Id == qualification.Id).Count();
                if (resp >0)
                {
                    _db.Entry(qualification).State = EntityState.Modified;
                }
                else
                {
                    qualification.Id = 0;
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
