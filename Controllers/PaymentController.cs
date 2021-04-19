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
    public class PaymentController : ControllerBase
    {

          private readonly Context _db;

        public PaymentController(Context context)
        {
            _db = context;
        }

        // GET api/Payment
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
        {
            return await _db.payments.ToListAsync();
        }

        // GET api/Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetSingle(long id)
        {
            var Payment = await _db.payments.FirstOrDefaultAsync(x => x.Id == id);
            if (Payment == null)
                return NotFound();

            return Payment;
        }

        // POST api/Payment
       [HttpPost]
        public async Task<ActionResult<Payment>> Post(Payment Payment)
        {
            _db.payments.Update(Payment);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Payment.Id }, Payment);
        }

        // PUT api/Payment/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Payment Payment)
        {
            if (id != Payment.Id)
                return BadRequest();
            _db.Entry(Payment).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Payment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Payment = await _db.payments.FindAsync(id);

            if (Payment == null)
                return NotFound();

            _db.payments.Remove(Payment);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
