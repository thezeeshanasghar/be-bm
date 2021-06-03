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
    public class ProcedureController : ControllerBase
    {

          private readonly Context _db;

        public ProcedureController(Context context)
        {
            _db = context;
        }

        // GET api/Procedures
        [HttpGet("get")]
         public async Task<Response<List<Procedure>>> GetAll(string? key)
        {
             List<Procedure> procedures;
            if (key != "" && key != null)
            {
                procedures= await _db.procedures.Where(x => x.Name.ToLower().Contains(key) || x.PerformedBy.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                procedures= await _db.procedures.ToListAsync();
            }
            return new Response<List<Procedure>>(true, "Successfully", procedures);
        }

        // GET api/Procedures/5
        [HttpGet("get/{id}")]
        public async Task<Response<Procedure>> GetSingle(long id)
        {
            var Procedures = await _db.procedures.FirstOrDefaultAsync(x => x.Id == id);
            if (Procedures == null)
               return new Response<Procedure>(false, "Record not found", null);

            return new Response<Procedure>(true, "operation succcessful", Procedures);
        }

        // POST api/Procedures
       [HttpPost("insert")]
        public async Task<ActionResult<Procedure>> Post(Procedure Procedures)
        {
            _db.procedures.Update(Procedures);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Procedures.Id }, Procedures);
        }

        // PUT api/Procedures/5
       [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(int id, Procedure Procedures)
        {
            if (id != Procedures.Id)
                return BadRequest();
            _db.Entry(Procedures).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Procedures/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Procedures = await _db.procedures.FindAsync(id);
            if (Procedures == null)
                return NotFound();
            _db.procedures.Remove(Procedures);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
