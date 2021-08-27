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
    public class PatientController : ControllerBase
    {
        private readonly Context _db;

        public PatientController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Patient>>> GetItems()
        {
            try
            {
                List<Patient> patientList = await _db.Patients.Include(x => x.User).ToListAsync();
                if (patientList != null && patientList.Count > 0)
                {
                    return new Response<List<Patient>>(true, "Success: Acquired data.", patientList);
                }
                return new Response<List<Patient>>(false, "Failure: Data does not exist.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Patient>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<Response<Patient>> GetItemById(int id)
        {
            try
            {
                Patient patient = await _db.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
                if (patient != null)
                {
                    return new Response<Patient>(true, "Success: Acquired data.", patient);
                }
                return new Response<Patient>(false, "Failure: Data doesnot exist.", null);
            }
            catch (Exception exception)
            {
                return new Response<Patient>(false, $"Server Failure: Unable to get object. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Patient>> InsertItem(PatientRequest patientRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                User user = new User();
                user.UserType = patientRequest.UserType;
                user.FirstName = patientRequest.FirstName;
                user.LastName = patientRequest.LastName;
                user.FatherHusbandName = patientRequest.FatherHusbandName;
                user.Gender = patientRequest.Gender;
                user.Cnic = patientRequest.Cnic;
                user.Contact = patientRequest.Contact;
                user.EmergencyContact = patientRequest.EmergencyContact;
                user.Email = patientRequest.Email;
                user.Address = patientRequest.Address;
                user.JoiningDate = patientRequest.JoiningDate;
                user.FloorNo = patientRequest.FloorNo;
                user.Experience = patientRequest.Experience;
                user.DateOfBirth = patientRequest.DateOfBirth;
                user.MaritalStatus = patientRequest.MaritalStatus;
                user.Religion = patientRequest.Religion;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                Patient patient = new Patient();
                patient.UserId = user.Id;
                patient.Category = patientRequest.Category;
                patient.BirthPlace = patientRequest.BirthPlace;
                patient.Type = patientRequest.Type;
                patient.ExternalId = patientRequest.ExternalId;
                patient.BloodGroup = patientRequest.BloodGroup;
                patient.ClinicSite = patientRequest.ClinicSite;
                patient.ReferredBy = patientRequest.ReferredBy;
                patient.ReferredDate = patientRequest.ReferredDate;
                patient.Guardian = patientRequest.Guardian;
                patient.PaymentProfile = patientRequest.PaymentProfile;
                patient.Description = patientRequest.Description;
                await _db.Patients.AddAsync(patient);
                await _db.SaveChangesAsync();

                Appointment appointment = new Appointment();
                appointment.PatientId = patient.Id;
                appointment.Code = patientRequest.AppointmentCode;
                appointment.Date = DateTime.UtcNow;
                appointment.ConsultationDate = patientRequest.ConsultationDate;
                appointment.Type = patientRequest.AppointmentType;
                await _db.Appointments.AddAsync(appointment);
                await _db.SaveChangesAsync();

                transaction.Commit();
                return new Response<Patient>(true, "Success: Created object.", patient);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Patient>(false, $"Server Failure: Unable to insert object. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Patient>> UpdateItem(int id, PatientRequest patientRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (id != patientRequest.Id)
                {
                    transaction.Rollback();
                    return new Response<Patient>(false, "Failure: Id sent in body does not match object Id", null);
                }

                Patient patient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id); ;
                if (patient == null)
                {
                    transaction.Rollback();
                    return new Response<Patient>(false, $"Failure: Unable to update patient {patientRequest.FirstName}. Because Id is invalid. ", null);
                }
                patient.Category = patientRequest.Category;
                patient.BirthPlace = patientRequest.BirthPlace;
                patient.Type = patientRequest.Type;
                patient.ExternalId = patientRequest.ExternalId;
                patient.BloodGroup = patientRequest.BloodGroup;
                patient.ClinicSite = patientRequest.ClinicSite;
                patient.ReferredBy = patientRequest.ReferredBy;
                patient.ReferredDate = patientRequest.ReferredDate;
                patient.Guardian = patientRequest.Guardian;
                patient.PaymentProfile = patientRequest.PaymentProfile;
                patient.Description = patientRequest.Description;
                await _db.SaveChangesAsync();

                User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == patient.UserId);
                if (user == null)
                {
                    transaction.Rollback();
                    return new Response<Patient>(false, "Failure: Data does not exist.", null);
                }
                user.UserType = patientRequest.UserType;
                user.FirstName = patientRequest.FirstName;
                user.LastName = patientRequest.LastName;
                user.FatherHusbandName = patientRequest.FatherHusbandName;
                user.Gender = patientRequest.Gender;
                user.Cnic = patientRequest.Cnic;
                user.Contact = patientRequest.Contact;
                user.EmergencyContact = patientRequest.EmergencyContact;
                user.Email = patientRequest.Email;
                user.Address = patientRequest.Address;
                user.JoiningDate = patientRequest.JoiningDate;
                user.FloorNo = patientRequest.FloorNo;
                user.Experience = patientRequest.Experience;
                user.DateOfBirth = patientRequest.DateOfBirth;
                user.MaritalStatus = patientRequest.MaritalStatus;
                user.Religion = patientRequest.Religion;
                await _db.SaveChangesAsync();

                transaction.Commit();
                return new Response<Patient>(true, "Success: Updated object.", patient);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Patient>(false, $"Server Failure: Unable to update object. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Patient>> DeleteItemById(int id)
        {
            try
            {
                Patient patient = await _db.Patients.FirstOrDefaultAsync(x => x.Id == id);
                if (patient == null)
                {
                    return new Response<Patient>(false, $"Failure: Object with id={id} does not exist.", null);
                }
                User user = await _db.Users.FindAsync(patient.UserId);
                if (user == null)
                {
                    return new Response<Patient>(false, $"Failure: Object with id={id} does not exist.", null);
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return new Response<Patient>(true, "Success: Deleted data.", patient);
            }
            catch (Exception exception)
            {
                return new Response<Patient>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }
    }
}
