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
    public class AppointmentController : ControllerBase
    {

          private readonly Context _db;

        public AppointmentController(Context context)
        {
            _db = context;
        }

        // GET api/Appointment
        [HttpGet]
           public async Task<Response<List<Appointment>>> GetAll(string? key)
        {
             List<Appointment> appointments;
            if (key != "" && key != null)
            {
                 appointments =  await _db.appointments.Where(x => x.Patient.Name.ToLower().Contains(key) || x.Patient.Sex.ToLower().Contains(key) || x.Patient.Email.ToLower().Contains(key) || x.Patient.City.ToLower().Contains(key) || x.Patient.LocalArea.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else {
                appointments = await _db.appointments.ToListAsync();
            }
             return new Response<List<Appointment>>(true, "Successfully", appointments);
           
        }

        // GET api/Appointment/5
        [HttpGet("{id}")]
         public async Task<Response<Appointment>> GetSingle(long id)
        {
            var Appointment = await _db.appointments.FirstOrDefaultAsync(x => x.Id == id);
            if (Appointment == null)
                     return new Response<Appointment>(false, "Record not found", null);
            return new Response<Appointment>(true, "operation succcessful", Appointment);   
        }

        // POST api/Appointment
       [HttpPost]
        public async Task<ActionResult<Appointment>> Post(Appointment Appointment)
        {
            _db.appointments.Update(Appointment);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Appointment.Id }, Appointment);
        }

        // PUT api/Appointment/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Appointment Appointment)
        {
            if (id != Appointment.Id)
                return BadRequest();
            _db.Entry(Appointment).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Appointment = await _db.appointments.FindAsync(id);

            if (Appointment == null)
                return NotFound();

            _db.appointments.Remove(Appointment);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
