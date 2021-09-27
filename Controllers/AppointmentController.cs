using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// Whole
// Category
// CategoryAndDoctor
// CategoryAndDate
// CategoryAndBooked
// CategoryAndDoctorAndDate
// CategoryAndDoctorAndBooked
// CategoryAndDateAndBooked
// CategoryAndDoctorAndDateAndBooked

// add receptionist id while creating appointment

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
                List<Appointment> appointmentList = await _db.Appointments.Include(x => x.Patient).Include(x => x.Patient.User).Take(30).OrderBy(x => x.Id).ToListAsync();
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
                List<Appointment> appointmentList = await _db.Appointments.Where(x => x.PatientCategory.Equals(category)).
                Include(x => x.Patient).Include(x => x.Patient.User).Take(30).OrderBy(x => x.Id).ToListAsync();
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
                List<Appointment> appointmentList = await _db.Appointments.Where(x => x.Id.ToString().Contains(search) || x.PatientId.ToString().Contains(search) ||
                x.DoctorId.ToString().Contains(search) || x.Code.Contains(search) ||
                x.Type.Contains(search) || x.PatientCategory.Contains(search) || x.Patient.ClinicSite.Contains(search) || x.Patient.Guardian.Contains(search) ||
                x.Patient.Description.Contains(search) || x.Patient.User.FirstName.Contains(search) || x.Patient.User.LastName.Contains(search) ||
                x.Patient.User.FatherHusbandName.Contains(search) || x.Patient.User.Cnic.Contains(search) || x.Patient.User.Contact.Contains(search) ||
                x.Patient.User.EmergencyContact.Contains(search) || x.Patient.User.FloorNo.ToString().Contains(search)).OrderBy(x => x.Id).Take(10).
                Include(x => x.Patient).Include(x => x.Patient.User).Take(10).OrderBy(x => x.Id).ToListAsync();
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

        [HttpPost("post/search")]
        public async Task<Response<List<Appointment>>> SearchItemByPost(AppointmentRequest request)
        {
            try
            {
                List<Appointment> appointmentList;
                if (request.searchFrom == null)
                {
                    return new Response<List<Appointment>>(false, "Failure: Select a valid <searchFrom> from the options: a) Whole b) Category c) CategoryAndDoctor d) CategoryAndDate e) CategoryAndBooked f) CategoryAndDoctorAndDate g) CategoryAndDoctorAndBooked h) CategoryAndDateAndBooked i) CategoryAndDoctorAndDateAndBooked ", null);
                }

                if (request.searchFrom.Equals("Category"))
                {
                    if (request.Category != null)
                    {
                        if (request.Category.Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategory(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> to search.", null);
                }
                else if (request.searchFrom.Equals("CategoryAndDoctor"))
                {
                    if (request.Category != null && request.Doctor != null)
                    {
                        if (request.Category.Length > 0 && request.Doctor.Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategoryAndDoctor(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> <doctor> to search.", null);
                }
                else if (request.searchFrom.Equals("CategoryAndDate"))
                {
                    if (request.Category != null && request.DateFrom != null && request.DateTo != null)
                    {
                        if (request.Category.Length > 0 && request.DateFrom.ToString().Length > 0 && request.DateTo.ToString().Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategoryAndDate(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> <date> to search.", null);
                }
                else if (request.searchFrom.Equals("CategoryAndBooked"))
                {
                    if (request.Category != null && request.DateFrom != null && request.DateTo != null)
                    {
                        if (request.Category.Length > 0 && request.DateFrom.ToString().Length > 0 && request.DateTo.ToString().Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategoryAndBooked(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> <date> to search.", null);
                }
                else if (request.searchFrom.Equals("CategoryAndDoctorAndDate"))
                {
                    if (request.Category != null && request.Doctor != null && request.DateFrom != null && request.DateTo != null)
                    {
                        if (request.Category.Length > 0 && request.Doctor.Length > 0 && request.DateTo.ToString().Length > 0 && request.DateFrom.ToString().Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategoryAndDoctorAndDate(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> <doctor> <date> to search.", null);
                }
                else if (request.searchFrom.Equals("CategoryAndDoctorAndBooked"))
                {
                    if (request.Category != null && request.Doctor != null && request.Booked != null)
                    {
                        if (request.Category.Length > 0 && request.Doctor.Length > 0 && request.Booked.Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategoryAndDoctorAndBooked(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> <doctor> <booked> to search.", null);
                }
                else if (request.searchFrom.Equals("CategoryAndDateAndBooked"))
                {
                    if (request.Category != null && request.DateFrom != null && request.DateTo != null && request.Booked != null)
                    {
                        if (request.Category.Length > 0 && request.DateFrom.ToString().Length > 0 && request.DateTo.ToString().Length > 0 && request.Booked.Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategoryAndDateAndBooked(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> to search.", null);
                }
                else if (request.searchFrom.Equals("CategoryAndDoctorAndDateAndBooked"))
                {
                    if (request.Category != null && request.Doctor != null && request.DateFrom != null && request.DateTo != null && request.Booked != null)
                    {
                        if (request.Category.Length > 0 && request.Doctor.Length > 0 && request.DateFrom.ToString().Length > 0 && request.DateTo.ToString().Length > 0 && request.Booked.Length > 0)
                        {
                            appointmentList = await searchAppointmentsByCategoryAndDoctorAndDateAndBooked(request);
                            if (appointmentList != null)
                            {
                                if (appointmentList.Count > 0)
                                {
                                    return new Response<List<Appointment>>(true, "Success: Acquired data.", appointmentList);
                                }
                            }
                            return new Response<List<Appointment>>(false, "Failure: Database is empty.", null);
                        }
                    }
                    return new Response<List<Appointment>>(false, "Failure: Select a valid value of parameter <category> to search.", null);
                }
                else
                {
                    return new Response<List<Appointment>>(false, "Failure: Select a valid <searchFrom> from the options: a) Whole b) Category c) CategoryAndDoctor d) CategoryAndDate e) CategoryAndBooked f) CategoryAndDoctorAndDate g) CategoryAndDoctorAndBooked h) CategoryAndDateAndBooked i) CategoryAndDoctorAndDateAndBooked ", null);
                }
            }
            catch (Exception exception)
            {
                return new Response<List<Appointment>>(false, $"Server Failure: Unable to acquire data. Because {exception.Message}", null);
            }
        }

        private async Task<List<Appointment>> searchAppointmentsByCategory(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) && x.PatientCategory.Equals(request.Category)).
                Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category)).Include(x => x.Patient).
            Include(x => x.Patient.User).Take(30).OrderBy(x => x.Id).ToListAsync();
            return list;
        }

        private async Task<List<Appointment>> searchAppointmentsByCategoryAndDoctor(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) && x.PatientCategory.Equals(request.Category) &&
                x.DoctorId.ToString().Equals(request.Doctor)).Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category) && x.DoctorId.ToString().Equals(request.Doctor)).Include(x => x.Patient).
            Include(x => x.Patient.User).Take(30).OrderBy(x => x.Id).ToListAsync();
            return list;
        }

        private async Task<List<Appointment>> searchAppointmentsByCategoryAndDate(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) &&
                x.PatientCategory.Equals(request.Category) && (x.Date >= request.DateFrom && x.Date < request.DateTo.AddDays(1))).
                Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category) && (x.Date >= request.DateFrom && x.Date < request.DateTo.AddDays(1))).
            Include(x => x.Patient).Include(x => x.Patient.User).Take(30).OrderBy(x => x.Id).ToListAsync();
            return list;
        }

        private async Task<List<Appointment>> searchAppointmentsByCategoryAndBooked(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) && x.PatientCategory.Equals(request.Category) &&
                x.ReceptionistId.ToString().Equals(request.Booked)).Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category) && x.ReceptionistId.
            ToString().Equals(request.Booked)).Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(30).ToListAsync();
            return list;
        }

        private async Task<List<Appointment>> searchAppointmentsByCategoryAndDoctorAndDate(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) && x.PatientCategory.Equals(request.Category) &&
                x.DoctorId.ToString().Equals(request.Doctor) && (x.Date >= request.DateFrom && x.Date < request.DateTo.AddDays(1))).
                Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category) &&
            x.DoctorId.ToString().Equals(request.Doctor) && (x.Date >= request.DateFrom && x.Date < request.DateTo.AddDays(1))).
            Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
            return list;
        }

        private async Task<List<Appointment>> searchAppointmentsByCategoryAndDoctorAndBooked(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) && x.PatientCategory.Equals(request.Category) &&
                x.DoctorId.ToString().Equals(request.Doctor) && x.ReceptionistId.ToString().Equals(request.Booked)).
                Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category) &&
            x.DoctorId.ToString().Equals(request.Doctor) && x.ReceptionistId.ToString().Equals(request.Booked)).
            Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
            return list;
        }

        private async Task<List<Appointment>> searchAppointmentsByCategoryAndDateAndBooked(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) && x.PatientCategory.Equals(request.Category) &&
                (x.Date >= request.DateFrom && x.Date < request.DateTo.AddDays(1)) && x.ReceptionistId.ToString().Equals(request.Booked)).
                OrderBy(x => x.Id).Take(10).Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category) &&
            x.DoctorId.ToString().Equals(request.Doctor) && x.ReceptionistId.ToString().Equals(request.Booked)).
            Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
            return list;
        }

        private async Task<List<Appointment>> searchAppointmentsByCategoryAndDoctorAndDateAndBooked(AppointmentRequest request)
        {
            List<Appointment> list;
            if (request.Search != null)
            {
                list = await _db.Appointments.Where(x => (x.Id.ToString().Contains(request.Search) || x.PatientId.ToString().Contains(request.Search) ||
                x.DoctorId.ToString().Contains(request.Search) || x.Code.Contains(request.Search) ||
                x.Type.Contains(request.Search) || x.PatientCategory.Contains(request.Search) ||
                x.Patient.ClinicSite.Contains(request.Search) || x.Patient.Guardian.Contains(request.Search) || x.Patient.Description.Contains(request.Search) ||
                x.Patient.User.FirstName.Contains(request.Search) || x.Patient.User.LastName.Contains(request.Search) ||
                x.Patient.User.FatherHusbandName.Contains(request.Search) || x.Patient.User.Cnic.Contains(request.Search) || x.Patient.User.Contact.Contains(request.Search) ||
                x.Patient.User.EmergencyContact.Contains(request.Search) || x.Patient.User.FloorNo.ToString().Contains(request.Search)) && x.PatientCategory.Equals(request.Category) &&
                x.DoctorId.ToString().Equals(request.Doctor) && (x.Date >= request.DateFrom && x.Date < request.DateTo.AddDays(1)) && x.ReceptionistId.ToString().Equals(request.Booked)).
                Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                return list;
            }
            list = await _db.Appointments.Where(x => x.PatientCategory.Equals(request.Category) &&
            x.DoctorId.ToString().Equals(request.Doctor) && (x.Date >= request.DateFrom && x.Date < request.DateTo.AddDays(1)) && x.ReceptionistId.ToString().Equals(request.Booked)).
            Include(x => x.Patient).Include(x => x.Patient.User).OrderBy(x => x.Id).Take(10).ToListAsync();
            return list;
        }
    }
}
