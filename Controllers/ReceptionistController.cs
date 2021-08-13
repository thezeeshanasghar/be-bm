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
    public class ReceptionistController : ControllerBase
    {
        private readonly Context _db;

        public ReceptionistController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Receptionist>>> GetItems()
        {
            try
            {
                List<Receptionist> receptionistList = await _db.Receptionists.ToListAsync();
                if (receptionistList != null && receptionistList.Count > 0)
                {
                    return new Response<List<Receptionist>>(true, "Success: Acquired data.", receptionistList);
                }
                else
                {
                    return new Response<List<Receptionist>>(false, "Failure: Data does not exist.", null);
                }
            }
            catch (Exception exception)
            {
                return new Response<List<Receptionist>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<Response<Receptionist>> GetItemById(long id)
        {
            try
            {
                Receptionist receptionist = await _db.Receptionists.FirstOrDefaultAsync(x => x.Id == id);
                if (receptionist != null)
                {
                    return new Response<Receptionist>(true, "Success: Acquired data.", receptionist);
                }
                else
                {
                    return new Response<Receptionist>(false, "Failure: Data doesnot exist.", null);
                }
            }
            catch (Exception exception)
            {
                return new Response<Receptionist>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Receptionist>> InsertItem(ReceptionistRequest receptionistRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                User user = new User();
                user.UserType = receptionistRequest.UserType;
                user.FirstName = receptionistRequest.FirstName;
                user.LastName = receptionistRequest.LastName;
                user.FatherHusbandName = receptionistRequest.FatherHusbandName;
                user.Gender = receptionistRequest.Gender;
                user.Cnic = receptionistRequest.Cnic;
                user.Contact = receptionistRequest.Contact;
                user.EmergencyContact = receptionistRequest.EmergencyContact;
                user.Email = receptionistRequest.Email;
                user.Address = receptionistRequest.Address;
                user.JoiningDate = receptionistRequest.JoiningDate;
                user.FloorNo = receptionistRequest.FloorNo;
                user.Experience = receptionistRequest.Experience;
                user.DateOfBirth = receptionistRequest.DateOfBirth;
                user.MaritalStatus = receptionistRequest.MaritalStatus;
                user.Religion = receptionistRequest.Religion;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                foreach (QualificationRequest drQualification in receptionistRequest.QualificationList)
                {
                    Qualification qualification = new Qualification();
                    qualification.UserId = user.Id;
                    qualification.Certificate = drQualification.Certificate;
                    qualification.Description = drQualification.Description;
                    qualification.QualificationType = drQualification.QualificationType;
                    await _db.Qualifications.AddAsync(qualification);
                    await _db.SaveChangesAsync();
                }

                Receptionist receptionist = new Receptionist();
                receptionist.UserId = user.Id;
                receptionist.JobType = receptionistRequest.JobType;
                receptionist.ShiftTime = receptionistRequest.ShiftTime;
                await _db.Receptionists.AddAsync(receptionist);
                await _db.SaveChangesAsync();

                transaction.Commit();
                return new Response<Receptionist>(true, "Success: Created object.", receptionist);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Receptionist>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Receptionist>> UpdateItem(long id, ReceptionistRequest receptionistRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                if (id != receptionistRequest.Id)
                {
                    return new Response<Receptionist>(false, "Failure: Id sent in body does not match object Id", null);
                }
                User user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new Response<Receptionist>(false, "Failure: Data doesnot exist.", null);
                }
                user.UserType = receptionistRequest.UserType;
                user.FirstName = receptionistRequest.FirstName;
                user.LastName = receptionistRequest.LastName;
                user.FatherHusbandName = receptionistRequest.FatherHusbandName;
                user.Gender = receptionistRequest.Gender;
                user.Cnic = receptionistRequest.Cnic;
                user.Contact = receptionistRequest.Contact;
                user.EmergencyContact = receptionistRequest.EmergencyContact;
                user.Email = receptionistRequest.Email;
                user.Address = receptionistRequest.Address;
                user.JoiningDate = receptionistRequest.JoiningDate;
                user.FloorNo = receptionistRequest.FloorNo;
                user.Experience = receptionistRequest.Experience;
                user.DateOfBirth = receptionistRequest.DateOfBirth;
                user.MaritalStatus = receptionistRequest.MaritalStatus;
                user.Religion = receptionistRequest.Religion;
                await _db.SaveChangesAsync();

                foreach (QualificationRequest drQualification in receptionistRequest.QualificationList)
                {
                    Qualification qualification = await _db.Qualifications.FirstOrDefaultAsync(x => x.Id == drQualification.Id);
                    if (qualification == null)
                    {
                        transaction.Rollback();
                        return new Response<Receptionist>(false, $"Failure: Unable to update qualification {drQualification.Certificate}. Because Id is invalid. ", null);
                    }
                    qualification.Certificate = drQualification.Certificate;
                    qualification.Description = drQualification.Description;
                    qualification.QualificationType = drQualification.QualificationType;
                    await _db.SaveChangesAsync();
                }

                Receptionist receptionist = await _db.Receptionists.FirstOrDefaultAsync(x => x.UserId == id); ;
                if (receptionist == null)
                {
                    transaction.Rollback();
                    return new Response<Receptionist>(false, $"Failure: Unable to update receptionist {receptionistRequest.FirstName}. Because Id is invalid. ", null);
                }
                receptionist.JobType = receptionistRequest.JobType;
                receptionist.ShiftTime = receptionistRequest.ShiftTime;

                await _db.SaveChangesAsync();
                transaction.Commit();

                return new Response<Receptionist>(true, "Success: Updated object.", receptionist);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Receptionist>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Receptionist>> DeleteItemById(int id)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    return new Response<Receptionist>(false, "Failure: Object doesnot exist.", null);
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return new Response<Receptionist>(true, "Success: Object deleted.", null);
            }
            catch (Exception exception)
            {
                return new Response<Receptionist>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }
    }
}
