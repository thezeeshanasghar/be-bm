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
    public class PatientController : ControllerBase
    {

          private readonly Context _db;

        public PatientController(Context context)
        {
            _db = context;
        }
        // GET api/Patient
        [HttpGet("Invoices")]
        public async Task<Response<List<PatientwithAppointment>>> GetInvices()
        {

            var query = await _db.patients
        .Join(
            _db.invoices,
            Patient => Patient.Id,
            invoice => invoice.Appointment.PatientId,
            (Patient, invoice) => new PatientwithAppointment()
            {
                Id = invoice.Id,
                Name = Patient.Name,
                FatherHusbandName = Patient.FatherHusbandName,
                Sex = Patient.Sex,
                Discount = invoice.Discount,
                NetAmount = invoice.NetAmount

            }).ToListAsync();

            return new Response<List<PatientwithAppointment>>(true, "Operation Successful", query);
        }
        // GET api/Patient
        [HttpGet("get")]
         public async Task<Response<List<Patient>>> GetAll(string? key)
        {
             List<Patient> patients;
            if (key != "" && key != null)
            {
                patients= await _db.patients.Where(x => x.Name.ToLower().Contains(key) || x.Email.ToLower().Contains(key) || x.Contact.ToLower().Contains(key) ||  x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                patients= await _db.patients.ToListAsync();
            }
             return new Response<List<Patient>>(true, "Successfully", patients);
        }

        // GET api/Patient/5
        [HttpGet("get/{id}")]
        public async Task<Response<Patient>> GetSingle(long id)
        {
            var Patient = await _db.patients.FirstOrDefaultAsync(x => x.Id == id);
            if (Patient == null)
                return new Response<Patient>(true, "Record not found", null);
             return new Response<Patient>(true, "operation succcessful", Patient);
        }

        // POST api/Patient
       [HttpPost("insert")]
        public async Task<ActionResult<Patient>> Post(Patient Patient)
        {
            _db.patients.Update(Patient);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Patient.Id }, Patient);
        }

        // PUT api/Patient/5
       [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(long id, Patient Patient)
        {
            if (id != Patient.Id)
                return BadRequest();
            _db.Entry(Patient).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Patient/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Patient = await _db.patients.FindAsync(id);

            if (Patient == null)
                return NotFound();

            _db.patients.Remove(Patient);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
