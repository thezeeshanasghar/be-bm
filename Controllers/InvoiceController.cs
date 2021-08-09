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
    public class InvoiceController : ControllerBase
    {

        private readonly Context _db;

        public InvoiceController(Context context)
        {
            _db = context;
        }

        // GET api/Invoice
        [HttpGet("get")]
        public async Task<Response<List<Invoice>>> GetAll(string key)
        {
            List<Invoice> InvoiceList;
            if (key != "" && key != null)
            {
                InvoiceList = await _db.Invoices.Where(x => x.Appointment.Patient.Name.ToLower().Contains(key) || x.Appointment.Patient.Email.ToLower().Contains(key) || x.Appointment.Patient.Contact.ToLower().Contains(key) || x.Appointment.Patient.City.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                InvoiceList = await _db.Invoices.ToListAsync();
            }
            return new Response<List<Invoice>>(true, "Successfully", InvoiceList);

        }

        // GET api/Invoice/5
        [HttpGet("get/{id}")]
        public async Task<Response<Invoice>> GetSingle(long id)
        {
            var Invoice = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            if (Invoice == null)
                return new Response<Invoice>(false, "Record not found", null);
            return new Response<Invoice>(true, "operation succcessful", Invoice);
        }

        // POST api/Invoice
        [HttpPost("insert")]
        public async Task<ActionResult<Invoice>> Post(Invoice Invoice)
        {
            _db.Invoices.Update(Invoice);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Invoice.Id }, Invoice);
        }

        // PUT api/Invoice/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(long id, Invoice Invoice)
        {
            if (id != Invoice.Id)
                return BadRequest();
            _db.Entry(Invoice).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Invoice/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Invoice = await _db.Invoices.FindAsync(id);

            if (Invoice == null)
                return NotFound();

            _db.Invoices.Remove(Invoice);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
