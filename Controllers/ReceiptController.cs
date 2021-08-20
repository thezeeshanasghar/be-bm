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

        // private readonly Context _db;

        // public ReceiptController(Context context)
        // {
        //     _db = context;
        // }

        // GET api/Receipt
        // [HttpGet("get")]
        // public async Task<Response<List<Receipt>>> GetAll(string key)
        // {
        //     List<Receipt> ReceiptList;
        //     if (key != "" && key != null)
        //     {
        //         ReceiptList = await _db.Receipts.Where(x => x.Name.ToLower().Contains(key) || x.Sex.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
        //     }
        //     else
        //     {
        //         ReceiptList = await _db.Receipts.ToListAsync();
        //     }
        //     return new Response<List<Receipt>>(true, "Successfully", ReceiptList);
        // }

        // // GET api/Receipt/5
        // [HttpGet("get/{id}")]
        // public async Task<Response<Receipt>> GetSingle(long id)
        // {
        //     var Receipt = await _db.Receipts.FirstOrDefaultAsync(x => x.Id == id);
        //     if (Receipt == null)
        //         return new Response<Receipt>(false, "Record not found", null);

        //     return new Response<Receipt>(true, "operation succcessful", Receipt);
        // }

        // // POST api/Receipt
        // [HttpPost("insert")]
        // public async Task<ActionResult<Receipt>> Post(Receipt Receipt)
        // {
        //     _db.Receipts.Update(Receipt);

        //     await _db.SaveChangesAsync();

        //     return CreatedAtAction(nameof(GetSingle), new { id = Receipt.Id }, Receipt);
        // }

        // // PUT api/Receipt/5
        // [HttpPut("update/{id}")]
        // public async Task<IActionResult> Put(long id, Receipt Receipt)
        // {
        //     if (id != Receipt.Id)
        //         return BadRequest();
        //     _db.Entry(Receipt).State = EntityState.Modified;
        //     await _db.SaveChangesAsync();

        //     return NoContent();
        // }

        // // DELETE api/Receipt/5
        // [HttpDelete("delete/{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var Receipt = await _db.Receipts.FindAsync(id);

        //     if (Receipt == null)
        //         return NotFound();

        //     _db.Receipts.Remove(Receipt);
        //     await _db.SaveChangesAsync();
        //     return NoContent();
        // }
    }
}
