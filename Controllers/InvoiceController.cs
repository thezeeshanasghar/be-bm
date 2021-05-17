﻿using System;
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
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Invoice>>> GetAll()
        {
            return await _db.invoices.ToListAsync();
        }

        // GET api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetSingle(long id)
        {
            var Invoice = await _db.invoices.FirstOrDefaultAsync(x => x.Id == id);
            if (Invoice == null)
                return NotFound();

            return Invoice;
        }

        // POST api/Invoice
       [HttpPost]
        public async Task<ActionResult<Invoice>> Post(Invoice Invoice)
        {
            _db.invoices.Update(Invoice);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Invoice.Id }, Invoice);
        }

        // PUT api/Invoice/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Invoice Invoice)
        {
            if (id != Invoice.Id)
                return BadRequest();
            _db.Entry(Invoice).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Invoice = await _db.invoices.FindAsync(id);

            if (Invoice == null)
                return NotFound();

            _db.invoices.Remove(Invoice);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}