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
        [HttpGet("get")]
        public async Task<Response<List<Payment>>> GetAll(string key)
        {
            List<Payment> PaymentList;
            if (key != "" && key != null)
            {
                PaymentList = await _db.Payments.Where(x => x.Name.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                PaymentList = await _db.Payments.ToListAsync();
            }
            return new Response<List<Payment>>(true, "Successfully", PaymentList);
        }

        // GET api/Payment/5
        [HttpGet("get/{id}")]
        public async Task<Response<Payment>> GetSingle(long id)
        {
            var Payment = await _db.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (Payment == null)
                return new Response<Payment>(false, "Record not found", null);

            return new Response<Payment>(true, "operation succcessful", Payment);
        }

        // POST api/Payment
        [HttpPost("insert")]
        public async Task<ActionResult<Payment>> Post(Payment Payment)
        {
            _db.Payments.Update(Payment);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Payment.Id }, Payment);
        }

        // PUT api/Payment/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(long id, Payment Payment)
        {
            if (id != Payment.Id)
                return BadRequest();
            _db.Entry(Payment).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Payment/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Payment = await _db.Payments.FindAsync(id);

            if (Payment == null)
                return NotFound();

            _db.Payments.Remove(Payment);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
