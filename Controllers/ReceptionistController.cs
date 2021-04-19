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
    public class ReceiptionistController : ControllerBase
    {

          private readonly Context _db;

        public ReceiptionistController(Context context)
        {
            _db = context;
        }

        // GET api/Receiptionist
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Receiptionist>>> GetAll()
        {
            return await _db.receiptionists.ToListAsync();
        }

        // GET api/Receiptionist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receiptionist>> GetSingle(long id)
        {
            var Receiptionist = await _db.receiptionists.FirstOrDefaultAsync(x => x.Id == id);
            if (Receiptionist == null)
                return NotFound();

            return Receiptionist;
        }

        // POST api/Receiptionist
       [HttpPost]
        public async Task<ActionResult<Receiptionist>> Post(Receiptionist Receiptionist)
        {
            _db.receiptionists.Update(Receiptionist);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Receiptionist.Id }, Receiptionist);
        }

        // PUT api/Receiptionist/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Receiptionist Receiptionist)
        {
            if (id != Receiptionist.Id)
                return BadRequest();
            _db.Entry(Receiptionist).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Receiptionist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Receiptionist = await _db.receiptionists.FindAsync(id);

            if (Receiptionist == null)
                return NotFound();

            _db.receiptionists.Remove(Receiptionist);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
