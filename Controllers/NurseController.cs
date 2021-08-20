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
    public class NurseController : ControllerBase
    {
        private readonly Context _db;

        public NurseController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Nurse>>> GetItems()
        {
            try
            {
                List<Nurse> doctorList = await _db.Nurses.ToListAsync();
                if (doctorList != null && doctorList.Count > 0)
                {
                    return new Response<List<Nurse>>(true, "Success: Acquired data.", doctorList);
                }
                return new Response<List<Nurse>>(false, "Failure: Data does not exist.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Nurse>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<Response<Nurse>> GetItemById(int id)
        {
            try
            {
                Nurse nurse = await _db.Nurses.FirstOrDefaultAsync(x => x.Id == id);
                if (nurse != null)
                {
                    return new Response<Nurse>(true, "Success: Acquired data.", nurse);
                }
                return new Response<Nurse>(false, "Failure: Data doesnot exist.", null);
            }
            catch (Exception exception)
            {
                return new Response<Nurse>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Nurse>> InsertItem(NurseRequest nurseRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                User user = new User();
                user.UserType = nurseRequest.UserType;
                user.FirstName = nurseRequest.FirstName;
                user.LastName = nurseRequest.LastName;
                user.FatherHusbandName = nurseRequest.FatherHusbandName;
                user.Gender = nurseRequest.Gender;
                user.Cnic = nurseRequest.Cnic;
                user.Contact = nurseRequest.Contact;
                user.EmergencyContact = nurseRequest.EmergencyContact;
                user.Email = nurseRequest.Email;
                user.Address = nurseRequest.Address;
                user.JoiningDate = nurseRequest.JoiningDate;
                user.FloorNo = nurseRequest.FloorNo;
                user.Experience = nurseRequest.Experience;
                user.DateOfBirth = nurseRequest.DateOfBirth;
                user.MaritalStatus = nurseRequest.MaritalStatus;
                user.Religion = nurseRequest.Religion;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                foreach (QualificationRequest drQualification in nurseRequest.QualificationList)
                {
                    Qualification qualification = new Qualification();
                    qualification.UserId = user.Id;
                    qualification.Certificate = drQualification.Certificate;
                    qualification.Description = drQualification.Description;
                    qualification.QualificationType = drQualification.QualificationType;
                    await _db.Qualifications.AddAsync(qualification);
                    await _db.SaveChangesAsync();
                }

                Nurse nurse = new Nurse();
                nurse.UserId = user.Id;
                nurse.DutyDuration = nurseRequest.DutyDuration;
                nurse.SharePercentage = nurseRequest.SharePercentage;
                nurse.Salary = nurseRequest.Salary;
                await _db.Nurses.AddAsync(nurse);
                await _db.SaveChangesAsync();

                transaction.Commit();
                return new Response<Nurse>(true, "Success: Created object.", nurse);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Nurse>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Nurse>> UpdateItem(int id, NurseRequest nurseRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (id != nurseRequest.Id)
                {
                    return new Response<Nurse>(false, "Failure: Id sent in body does not match object Id", null);
                }
                User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new Response<Nurse>(false, "Failure: Data doesnot exist.", null);
                }
                user.UserType = nurseRequest.UserType;
                user.FirstName = nurseRequest.FirstName;
                user.LastName = nurseRequest.LastName;
                user.FatherHusbandName = nurseRequest.FatherHusbandName;
                user.Gender = nurseRequest.Gender;
                user.Cnic = nurseRequest.Cnic;
                user.Contact = nurseRequest.Contact;
                user.EmergencyContact = nurseRequest.EmergencyContact;
                user.Email = nurseRequest.Email;
                user.Address = nurseRequest.Address;
                user.JoiningDate = nurseRequest.JoiningDate;
                user.FloorNo = nurseRequest.FloorNo;
                user.Experience = nurseRequest.Experience;
                user.DateOfBirth = nurseRequest.DateOfBirth;
                user.MaritalStatus = nurseRequest.MaritalStatus;
                user.Religion = nurseRequest.Religion;
                await _db.SaveChangesAsync();

                foreach (QualificationRequest drQualification in nurseRequest.QualificationList)
                {
                    Qualification qualification = await _db.Qualifications.FirstOrDefaultAsync(x => x.Id == drQualification.Id);
                    if (qualification == null)
                    {
                        transaction.Rollback();
                        return new Response<Nurse>(false, $"Failure: Unable to update qualification {drQualification.Certificate}. Because Id is invalid. ", null);
                    }
                    qualification.Certificate = drQualification.Certificate;
                    qualification.Description = drQualification.Description;
                    qualification.QualificationType = drQualification.QualificationType;
                    await _db.SaveChangesAsync();
                }

                Nurse nurse = await _db.Nurses.FirstOrDefaultAsync(x => x.UserId == id); ;
                if (nurse == null)
                {
                    transaction.Rollback();
                    return new Response<Nurse>(false, $"Failure: Unable to update Nurse {nurseRequest.FirstName}. Because Id is invalid. ", null);
                }
                nurse.DutyDuration = nurseRequest.DutyDuration;
                nurse.SharePercentage = nurseRequest.SharePercentage;
                nurse.Salary = nurseRequest.Salary;

                await _db.SaveChangesAsync();
                transaction.Commit();

                return new Response<Nurse>(true, "Success: Updated object.", nurse);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Nurse>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Nurse>> DeleteItemById(int id)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    return new Response<Nurse>(false, "Failure: Object doesnot exist.", null);
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return new Response<Nurse>(true, "Success: Object deleted.", null);
            }
            catch (Exception exception)
            {
                return new Response<Nurse>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }
    }
}
