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
                List<Doctor> doctorList = await _db.Doctors.ToListAsync();
                if (doctorList != null && doctorList.Count > 0)
                {
                    return new Response<List<Doctor>>(true, "Success: Acquired data.", doctorList);
                }
                else
                {
                    return new Response<List<Doctor>>(false, "Failure: Data does not exist.", null);
                }
            }
            catch (Exception exception)
            {
                return new Response<List<Doctor>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<Response<Doctor>> GetItemById(long id)
        {
            try
            {
                Doctor doctorObject = await _db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
                if (doctorObject != null)
                {
                    return new Response<Doctor>(true, "Success: Acquired data.", doctorObject);
                }
                else
                {
                    return new Response<Doctor>(false, "Failure: Data doesnot exist.", null);
                }
            }
            catch (Exception exception)
            {
                return new Response<Doctor>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Doctor>> InsertItem(DoctorRequest doctorRequest)
        {
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
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

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

                Doctor doctor = new Doctor();
                doctor.UserId = user.Id;
                doctor.ConsultationFee = doctorRequest.ConsultationFee;
                doctor.EmergencyConsultationFee = doctorRequest.EmergencyConsultationFee;
                doctor.ShareInFee = doctorRequest.ShareInFee;
                doctor.SpecialityType = doctorRequest.SpecialityType;
                await _db.Doctors.AddAsync(doctor);
                await _db.SaveChangesAsync();

                return new Response<Doctor>(true, "Success: Created object.", doctor);
            }
            catch (Exception exception)
            {
                return new Response<Doctor>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Doctor>> UpdateItem(long id, DoctorRequest doctorRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (id != doctorRequest.Id)
                {
                    return new Response<Doctor>(false, "Failure: Id sent in body does not match object Id", null);
                }
                User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new Response<Doctor>(false, "Failure: Data doesnot exist.", null);
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
                await _db.SaveChangesAsync();

                foreach (QualificationRequest drQualification in doctorRequest.QualificationList)
                {
                    Qualification qualification = await _db.Qualifications.FirstOrDefaultAsync(x => x.Id == drQualification.Id);
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

                Doctor doctor = await _db.Doctors.FirstOrDefaultAsync(x => x.UserId == id); ;
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
                transaction.Commit();
                
                return new Response<Doctor>(true, "Success: Updated object.", doctor);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Doctor>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Doctor>> DeleteItemById(int id)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    return new Response<Doctor>(false, "Failure: Object doesnot exist.", null);
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return new Response<Doctor>(true, "Success: Object deleted.", null);
            }
            catch (Exception exception)
            {
                return new Response<Doctor>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }
    }
}
