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
    public class ReceiptController : ControllerBase
    {

          private readonly Context _db;

        public ReceiptController(Context context)
        {
            _db = context;
        }

        // GET api/Receipt
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Receipt>>> GetAll()
        {
            return await _db.receipts.ToListAsync();
        }

        // GET api/Receipt/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetSingle(long id)
        {
            var Receipt = await _db.receipts.FirstOrDefaultAsync(x => x.Id == id);
            if (Receipt == null)
                return NotFound();

            return Receipt;
        }

        // POST api/Receipt
       [HttpPost]
        public async Task<ActionResult<Receipt>> Post(Receipt Receipt)
        {
            _db.receipts.Update(Receipt);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Receipt.Id }, Receipt);
        }

        // PUT api/Receipt/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Receipt Receipt)
        {
            if (id != Receipt.Id)
                return BadRequest();
            _db.Entry(Receipt).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Receipt/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Receipt = await _db.receipts.FindAsync(id);

            if (Receipt == null)
                return NotFound();

            _db.receipts.Remove(Receipt);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
