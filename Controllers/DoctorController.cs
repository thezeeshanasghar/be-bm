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
    // [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly Context _db;

        public DoctorController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Doctor>>> GetItems()
        {
            try
            {
                List<Doctor> doctorList = await _db.Doctors.Include(x => x.User).Include(x => x.User.Qualifications).ToListAsync();
                if (doctorList != null)
                {
                    if (doctorList.Count > 0)
                    {
                        return new Response<List<Doctor>>(true, "Success: Acquired data.", doctorList);
                    }
                }
                return new Response<List<Doctor>>(false, "Failure: Database is empty.", null);

            }
            catch (Exception exception)
            {
                return new Response<List<Doctor>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/id/{id}")]
        public async Task<Response<Doctor>> GetItemById(int id)
        {
            try
            {
                Doctor doctor = await _db.Doctors.Include(x => x.User).Include(x => x.User.Qualifications).FirstOrDefaultAsync(x => x.Id == id);
                if (doctor == null)
                {
                    return new Response<Doctor>(false, "Failure: Data doesn't exist.", null);
                }
                return new Response<Doctor>(true, "Success: Acquired data.", doctor);
            }
            catch (Exception exception)
            {
                return new Response<Doctor>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("search/{search}")]
        public async Task<Response<List<Doctor>>> SearchItems(String search)
        {
            try
            {
                if (String.IsNullOrEmpty(search))
                {
                    return new Response<List<Doctor>>(false, "Failure: Enter a valid search string.", null);
                }
                List<Doctor> doctorList = await _db.Doctors.Where(x => x.Id.ToString().Contains(search) || x.UserId.ToString().Contains(search) || 
                x.ConsultationFee.ToString().Contains(search) || x.EmergencyConsultationFee.ToString().Contains(search) || x.ShareInFee.ToString().Contains(search) || 
                x.SpecialityType.Contains(search) || x.User.FirstName.Contains(search) || x.User.LastName.Contains(search) || x.User.FatherHusbandName.Contains(search) || 
                x.User.Gender.Contains(search) || x.User.Cnic.Contains(search) || x.User.Contact.Contains(search) || x.User.EmergencyContact.Contains(search) || 
                x.User.Email.Contains(search) || x.User.Address.Contains(search) || x.User.Experience.Contains(search) || 
                x.User.FloorNo.ToString().Contains(search)).OrderBy(x => x.Id).Take(10).Include(x => x.User).ToListAsync();
                if (doctorList != null)
                {
                    if (doctorList.Count > 0)
                    {
                        return new Response<List<Doctor>>(true, "Success: Acquired data.", doctorList);
                    }
                }
                return new Response<List<Doctor>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Doctor>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Doctor>> InsertItem(DoctorRequest doctorRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                User user = new User();
                user.UserType = doctorRequest.UserType;
                user.FirstName = doctorRequest.FirstName;
                user.LastName = doctorRequest.LastName;
                user.FatherHusbandName = doctorRequest.FatherHusbandName;
                user.Gender = doctorRequest.Gender;
                user.Cnic = doctorRequest.Cnic;
                user.Contact = doctorRequest.Contact;
                user.EmergencyContact = doctorRequest.EmergencyContact;
                user.Email = doctorRequest.Email;
                user.Address = doctorRequest.Address;
                user.JoiningDate = doctorRequest.JoiningDate;
                user.FloorNo = doctorRequest.FloorNo;
                user.Experience = doctorRequest.Experience;
                user.DateOfBirth = doctorRequest.DateOfBirth;
                user.MaritalStatus = doctorRequest.MaritalStatus;
                user.Religion = doctorRequest.Religion;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                if (doctorRequest.QualificationList != null)
                {
                    if (doctorRequest.QualificationList.Count > 0)
                    {
                        foreach (QualificationRequest drQualification in doctorRequest.QualificationList)
                        {
                            Qualification qualification = new Qualification();
                            qualification.UserId = user.Id;
                            qualification.Certificate = drQualification.Certificate;
                            qualification.Description = drQualification.Description;
                            qualification.QualificationType = drQualification.QualificationType;
                            await _db.Qualifications.AddAsync(qualification);
                            await _db.SaveChangesAsync();
                        }
                    }
                }

                Doctor doctor = new Doctor();
                doctor.UserId = user.Id;
                doctor.ConsultationFee = doctorRequest.ConsultationFee;
                doctor.EmergencyConsultationFee = doctorRequest.EmergencyConsultationFee;
                doctor.ShareInFee = doctorRequest.ShareInFee;
                doctor.SpecialityType = doctorRequest.SpecialityType;
                await _db.Doctors.AddAsync(doctor);
                await _db.SaveChangesAsync();

                transaction.Commit();
                return new Response<Doctor>(true, "Success: Inserted data.", doctor);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Doctor>(false, $"Server Failure: Unable to insert data. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Doctor>> UpdateItem(int id, DoctorRequest doctorRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (id != doctorRequest.Id)
                {
                    transaction.Rollback();
                    return new Response<Doctor>(false, "Failure: Id sent in body does not match object Id", null);
                }

                Doctor doctor = await _db.Doctors.Include(x => x.User.Qualifications).FirstOrDefaultAsync(x => x.Id == id);
                if (doctor == null)
                {
                    transaction.Rollback();
                    return new Response<Doctor>(false, $"Failure: Unable to update doctor {doctorRequest.FirstName}. Because Id is invalid. ", null);
                }
                doctor.ConsultationFee = doctorRequest.ConsultationFee;
                doctor.EmergencyConsultationFee = doctorRequest.EmergencyConsultationFee;
                doctor.ShareInFee = doctorRequest.ShareInFee;
                doctor.SpecialityType = doctorRequest.SpecialityType;
                await _db.SaveChangesAsync();

                if (doctorRequest.QualificationList != null)
                {
                    if (doctorRequest.QualificationList.Count > 0)
                    {
                        foreach (QualificationRequest drQualification in doctorRequest.QualificationList)
                        {
                            Qualification qualification = await _db.Qualifications.FirstOrDefaultAsync(x => x.Id == drQualification.Id && x.UserId == doctor.UserId);
                            if (qualification == null)
                            {
                                transaction.Rollback();
                                return new Response<Doctor>(false, $"Failure: Unable to update qualification {drQualification.Certificate}. Because Id is invalid. ", null);
                            }
                            qualification.Certificate = drQualification.Certificate;
                            qualification.Description = drQualification.Description;
                            qualification.QualificationType = drQualification.QualificationType;
                            await _db.SaveChangesAsync();
                        }
                    }
                }

                User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == doctor.UserId);
                if (user == null)
                {
                    transaction.Rollback();
                    return new Response<Doctor>(false, "Failure: Data doesn't exist.", null);
                }
                user.UserType = doctorRequest.UserType;
                user.FirstName = doctorRequest.FirstName;
                user.LastName = doctorRequest.LastName;
                user.FatherHusbandName = doctorRequest.FatherHusbandName;
                user.Gender = doctorRequest.Gender;
                user.Cnic = doctorRequest.Cnic;
                user.Contact = doctorRequest.Contact;
                user.EmergencyContact = doctorRequest.EmergencyContact;
                user.Email = doctorRequest.Email;
                user.Address = doctorRequest.Address;
                user.JoiningDate = doctorRequest.JoiningDate;
                user.FloorNo = doctorRequest.FloorNo;
                user.Experience = doctorRequest.Experience;
                user.DateOfBirth = doctorRequest.DateOfBirth;
                user.MaritalStatus = doctorRequest.MaritalStatus;
                user.Religion = doctorRequest.Religion;
                await _db.SaveChangesAsync();

                transaction.Commit();
                return new Response<Doctor>(true, "Success: Updated data.", doctor);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Doctor>(false, $"Server Failure: Unable to update data. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Doctor>> DeleteItemById(int id)
        {
            try
            {
                Doctor doctor = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
                if (doctor == null)
                {
                    return new Response<Doctor>(false, $"Failure: Object with id={id} does not exist.", null);
                }
                User user = await _db.Users.FindAsync(doctor.UserId);
                if (user == null)
                {
                    return new Response<Doctor>(false, $"Failure: Object with id={id} does not exist.", null);
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return new Response<Doctor>(true, "Success: Deleted data.", doctor);
            }
            catch (Exception exception)
            {
                return new Response<Doctor>(false, $"Server Failure: Unable to delete data. Because {exception.Message}", null);
            }
        }
    }
}
