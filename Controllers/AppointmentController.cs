using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly Context _db;

        public AppointmentController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Appointment>>> GetItems()
        {
            try
            {
                List<Appointment> appointmentList = await _db.Appointments.Include(x => x.Patient).Include(x => x.Patient.User).ToListAsync();
                if (appointmentList != null)
                {
                    if (appointmentList.Count > 0)
                    {
                        return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                    }
                }
                return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Appointment>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/id/{id}")]
        public async Task<Response<Appointment>> GetItemById(int id)
        {
            try
            {
                Appointment appointment = await _db.Appointments.Include(x => x.Patient).Include(x => x.Patient.User).FirstOrDefaultAsync(x => x.Id == id);
                if (appointment == null)
                {
                    return new Response<Appointment>(false, "Failure: Data doesn't exist.", null);
                }
                return new Response<Appointment>(true, "Success: Acquired data.", appointment);
            }
            catch (Exception exception)
            {
                return new Response<Appointment>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/category/{category}")]
        public async Task<Response<List<Appointment>>> GetItemByCategory(String category)
        {
            try
            {
                List<Appointment> appointmentList = await _db.Appointments.Where(x => x.PatientCategory.Equals(category)).Include(x => x.Patient).Include(x => x.Patient.User).ToListAsync();
                if (appointmentList != null)
                {
                    if (appointmentList.Count > 0)
                    {
                        return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                    }
                }
                return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Appointment>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("search/{search}")]
        public async Task<Response<List<Appointment>>> SearchItems(String search)
        {
            try
            {
                if (String.IsNullOrEmpty(search))
                {
                    return new Response<List<Appointment>>(false, "Failure: Enter a valid search string.", null);
                }
                List<Appointment> appointmentList = await _db.Appointments.Where(x => x.Id.ToString().Contains(search) || x.PatientId.ToString().Contains(search) || x.DoctorId.ToString().Contains(search) || x.Code.Contains(search) || x.Date.ToString().Contains(search) || x.ConsultationDate.ToString().Contains(search) || x.Type.Contains(search) || x.PatientCategory.Contains(search) || x.Patient.ClinicSite.Contains(search) || x.Patient.Guardian.Contains(search) || x.Patient.Description.Contains(search) || x.Patient.User.FirstName.Contains(search) || x.Patient.User.LastName.Contains(search) || x.Patient.User.FatherHusbandName.Contains(search) || x.Patient.User.Cnic.Contains(search) || x.Patient.User.Contact.Contains(search) || x.Patient.User.EmergencyContact.Contains(search) || x.Patient.User.FloorNo.ToString().Contains(search)).OrderBy(x => x.Id).Take(10).Include(x => x.Patient).Include(x => x.Patient.User).ToListAsync();
                if (appointmentList != null)
                {
                    if (appointmentList.Count > 0)
                    {
                        return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                    }
                }
                return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Appointment>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        // [HttpPost("insert")]
        // public async Task<Response<Appointment>> InsertItem(ServiceRequest serviceRequest)
        // {
        //     try
        //     {
        //         Appointment appointment = new Appointment();
        //         appointment.Name = serviceRequest.Name;
        //         appointment.Description = serviceRequest.Description;
        //         await _db.Appointments.AddAsync(appointment);
        //         await _db.SaveChangesAsync();

        //         return new Response<Appointment>(true, "Success: Inserted data.", appointment);
        //     }
        //     catch (Exception exception)
        //     {
        //         return new Response<Appointment>(false, $"Server Failure: Unable to insert data. Because {exception.Message}", null);
        //     }
        // }

        // [HttpPut("update/{id}")]
        // public async Task<Response<Appointment>> UpdateItem(int id, ServiceRequest serviceRequest)
        // {
        //     try
        //     {
        //         if (id != serviceRequest.Id)
        //         {
        //             return new Response<Appointment>(false, "Failure: Id sent in body does not match object Id", null);
        //         }
        //         Appointment appointment = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        //         if (appointment == null)
        //         {
        //             return new Response<Appointment>(false, "Failure: Data doesn't exist.", null);
        //         }
        //         appointment.Name = serviceRequest.Name;
        //         appointment.Description = serviceRequest.Description;
        //         await _db.SaveChangesAsync();

        //         return new Response<Appointment>(true, "Success: Updated data.", appointment);
        //     }
        //     catch (Exception exception)
        //     {
        //         return new Response<Appointment>(false, $"Server Failure: Unable to update data. Because {exception.Message}", null);
        //     }
        // }

        // [HttpDelete("delete/{id}")]
        // public async Task<Response<Appointment>> DeleteItemById(int id)
        // {
        //     try
        //     {
        //         Appointment appointment = await _db.Appointments.FindAsync(id);
        //         if (appointment == null)
        //         {
        //             return new Response<Appointment>(false, "Failure: Object doesn't exist.", null);
        //         }
        //         _db.Appointments.Remove(appointment);
        //         await _db.SaveChangesAsync();

        //         return new Response<Appointment>(true, "Success: Deleted data.", appointment);
        //     }
        //     catch (Exception exception)
        //     {
        //         return new Response<Appointment>(false, $"Server Failure: Unable to delete data. Because {exception.Message}", null);
        //     }
        // }
    }
}
