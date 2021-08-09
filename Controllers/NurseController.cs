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
        [HttpGet("get")]
        public async Task<Response<List<Nurse>>> GetAll(String key)
        {
            List<Nurse> NurseList;
            if (key != "" && key != null)
            {
                NurseList = await _db.Nurses.Include(x => x.UserObject).ThenInclude(x => x.Qualifications).Where(x => x.UserObject.FirstName.ToLower().Contains(key) || x.UserObject.LastName.ToLower().Contains(key) || x.UserObject.Email.ToLower().Contains(key) || x.UserObject.Contact.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                NurseList = await _db.Nurses.Include(x => x.UserObject).ThenInclude(x => x.Qualifications).ToListAsync();
            }
            return new Response<List<Nurse>>(true, "Successfully", NurseList);
        }

        // GET api/Nurse/5
        [HttpGet("get/{id}")]
        public async Task<Response<Nurse>> GetSingle(long id)
        {
            var Nurse = await _db.Nurses.Include(x => x.UserObject).ThenInclude(x => x.Qualifications).FirstOrDefaultAsync(x => x.Id == id);
            if (Nurse == null)
                return new Response<Nurse>(false, "Record not found", null);
            return new Response<Nurse>(true, "operation succcessful", Nurse);
        }

        // POST api/Nurse
        [HttpPost("insert")]
        public async Task<ActionResult<Nurse>> Post(Nurse Nurse)
        {

            _db.Nurses.Update(Nurse);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Nurse.Id }, Nurse);
        }

        // PUT api/Nurse/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(int id, Nurse Nurse)
        {
            // using var transaction = _db.Database.BeginTransaction();
            // try
            // {
            //     if (id != Nurse.Id)
            //         return BadRequest();

            //     _db.Entry(Nurse).State = EntityState.Modified;

            //     _db.Entry(Nurse.UserObject).State = EntityState.Modified;
            //     foreach (Qualifications qualification in Nurse.UserObject.Qualifications)
            //     {
            //         var resp = _db.Qualifications.Where(x => x.Id == qualification.Id).Count();
            //         if (resp > 0)
            //         {
            //             _db.Entry(qualification).State = EntityState.Modified;
            //         }
            //         else
            //         {
            //             qualification.Id = 0;
            //             _db.Qualifications.Update(qualification);
            //         }

            //         await _db.SaveChangesAsync();
            //     }
            //     await _db.SaveChangesAsync();
            //     transaction.Commit();

            // }
            // catch (Exception ex)
            // {
            //     transaction.Rollback();
            // }

            return NoContent();

        }

        // DELETE api/Nurse/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Nurse = await _db.Nurses.FindAsync(id);

            if (Nurse == null)
                return NotFound();

            _db.Nurses.Remove(Nurse);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
