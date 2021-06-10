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
    public class PatientController : ControllerBase
    {

        private readonly Context _db;

        public PatientController(Context context)
        {
            _db = context;
        }
        // GET api/Patient
        [HttpGet("Invoices")]
        public Response<List<PatientwithAppointment>> GetInvices(string? Category)
        {

            var query = from b in _db.Set<Patient>()
                        join p in _db.Set<Appointment>()
                            on b.Id equals p.PatientId 
                         join s in _db.Set<Invoice>()
                          on p.Id equals s.AppointmentId
                            into grouping
                        from s in grouping.DefaultIfEmpty()
                        select new PatientwithAppointment()
                        {
                            Id = p == null ? 0 : p.Id,
                            Name = b.Name,
                            PatientId = b.Id,
                            Category = p.AppointmentType,
                            FatherHusbandName = b.FatherHusbandName,
                            Sex = b.Sex,
                            Discount = s == null ? 0 : s.Discount,
                            NetAmount = s == null ? 0 : s.NetAmount,
                            AppointmentId = p == null?0:p.Id,
                            Area=b.LocalArea,
                            City=b.City,
                            Contact=b.Contact,
                            Dob=b.Dob,
                            Email=b.Email
                        };

            var Invoices=new List<PatientwithAppointment>();
            if (Category!=null)
            {
                Invoices=(query).Where(x => x.Category == Category).ToList();
            }
            else {
                Invoices = (query).ToList();
            }


            return new Response<List<PatientwithAppointment>>(true, "List Generated Syccessfully", Invoices);
        }
        // GET api/Patient
        [HttpGet("Invoices/get/{Id}")]
        public  Response<PatientwithAppointment> GetPatientByIds(int Id)
        {

            var query = _db.patients.Where(x => x.Id == Id).Include(x => x.Appointments).ThenInclude(x => x.Invoices).ToList();
            PatientwithAppointment PatientwithAppointment = new PatientwithAppointment();
            foreach (var item in query)
            {
                Appointment LastApointment = item.Appointments.OrderByDescending(x => x.Id).FirstOrDefault();

                var LastInvoice =new Invoice();
                if (LastApointment != null)
                {
                    LastInvoice = LastApointment.Invoices.OrderByDescending(x => x.Id).FirstOrDefault();
                }
                else {
                    LastInvoice = null;
                }
                PatientwithAppointment = new PatientwithAppointment()
                {
                    AppointmentId = LastApointment != null ? LastApointment.Id : 0,
                    Category =item.PatientCategory,
                    Discount = LastInvoice != null ? LastInvoice.Discount : 0,
                    FatherHusbandName =item.FatherHusbandName,
                    LastAppointmentDate = LastApointment != null ? LastApointment.AppointmentDate : DateTime.Now,
                    Id = LastInvoice != null ? LastInvoice.Id : 0,
                    Name = item.Name,
                    NetAmount = LastInvoice != null ? LastInvoice.NetAmount : 0,
                    PatientId =item.Id,
                    Sex=item.Sex,
                    Area=item.LocalArea,
                    City=item.City,
                    Contact=item.Contact,
                    Dob=item.Dob,
                    Email=item.Email
                };
            }

            return new Response<PatientwithAppointment>(true, "List Generated Syccessfully", PatientwithAppointment);

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
       [HttpPost("insert/{AppointmentType}")]
        public async Task<ActionResult<Patient>> Post(Patient Patient,String AppointmentType)
        {
          string Code =  DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            Appointment appointments = new Appointment();
            List<Appointment> appointmentList = new List<Appointment>();

            appointments.AppointmentType = AppointmentType;
            appointments.AppointmentDate = DateTime.Now;
            appointments.AppointmentCode = Code;
            appointmentList.Add(appointments);

            Patient.Appointments= appointmentList;

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
