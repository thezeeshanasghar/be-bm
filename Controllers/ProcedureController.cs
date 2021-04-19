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
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Procedures>>> GetAll()
        {
            return await _db.procedures.ToListAsync();
        }

        // GET api/Procedures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedures>> GetSingle(long id)
        {
            var Procedures = await _db.procedures.FirstOrDefaultAsync(x => x.Id == id);
            if (Procedures == null)
                return NotFound();

            return Procedures;
        }

        // POST api/Procedures
       [HttpPost]
        public async Task<ActionResult<Procedures>> Post(Procedures Procedures)
        {
            _db.procedures.Update(Procedures);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Procedures.Id }, Procedures);
        }

        // PUT api/Procedures/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Procedures Procedures)
        {
            if (id != Procedures.Id)
                return BadRequest();
            _db.Entry(Procedures).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Procedures/5
        [HttpDelete("{id}")]
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
