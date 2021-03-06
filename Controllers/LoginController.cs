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
    public class LoginController : ControllerBase
    {
        private readonly Context _db;

        public LoginController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Login>>> GetItems()
        {
            try
            {
                List<Login> loginList = await _db.Login.ToListAsync();
                if (loginList != null)
                {
                    if (loginList.Count > 0)
                    {
                        return new Response<List<Login>>(true, "Success: Acquired data.", loginList);
                    }
                }
                return new Response<List<Login>>(false, "Failure: Data does not exist.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Login>>(false, "Server Failure: Unable to get data. Because " + exception.Message, null);
            }
        }

        [HttpGet("get/id/{id}")]
        public async Task<Response<Login>> GetItemById(int id)
        {
            try
            {
                Login loginObject = await _db.Login.FirstOrDefaultAsync(x => x.Id == id);
                if (loginObject == null)
                {
                    return new Response<Login>(false, "Failure: Data doesnot exist.", null);
                }
                return new Response<Login>(true, "Success: Acquired data.", loginObject);
            }
            catch (Exception exception)
            {
                return new Response<Login>(false, "Server Failure: Unable to get data. Because " + exception.Message, null);
            }
        }

        [HttpGet("search/{search}")]
        public async Task<Response<List<Login>>> SearchItems(String search)
        {
            try
            {
                if (String.IsNullOrEmpty(search))
                {
                    return new Response<List<Login>>(false, "Failure: Enter a valid search string.", null);
                }
                List<Login> loginList = await _db.Login.Where(x => x.Id.ToString().Contains(search) || x.UserId.ToString().Contains(search) || x.UserName.Contains(search) || x.Password.Contains(search)).OrderBy(x => x.Id).Take(10).ToListAsync();
                if (loginList != null)
                {
                    if (loginList.Count > 0)
                    {
                        return new Response<List<Login>>(true, "Success: Acquired data.", loginList);
                    }
                }
                return new Response<List<Login>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Login>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Login>> InsertItem(Login loginObject)
        {
            try
            {
                User userObject = await _db.Users.FirstOrDefaultAsync(x => x.Id == loginObject.UserId);
                if (userObject == null)
                {
                    return new Response<Login>(false, "Failure: Invalid user id or user does not exist.", null);
                }
                await _db.Login.AddAsync(loginObject);
                await _db.SaveChangesAsync();

                return new Response<Login>(true, "Success: Created object.", loginObject);
            }
            catch (Exception exception)
            {
                return new Response<Login>(false, "Server Failure: Unable to create object. Because " + exception.Message, null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Login>> UpdateItem(int id, Login loginObject)
        {
            if (id != loginObject.Id)
            {
                return new Response<Login>(false, "Failure: Id sent in body does not match object Id", null);
            }
            try
            {
                Login existingItem = await _db.Login.FirstOrDefaultAsync(x => x.Id == id);
                if (existingItem == null)
                {
                    return new Response<Login>(false, "Failure: Object doesnot exist.", null);
                }
                existingItem = loginObject;
                await _db.SaveChangesAsync();

                return new Response<Login>(true, "Success: Updated object.", loginObject);
            }
            catch (Exception exception)
            {
                return new Response<Login>(false, "API Failure: Unable to update object. Because " + exception.Message, null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Login>> DeleteItemById(int id)
        {
            try
            {
                Login loginObject = await _db.Login.FindAsync(id);
                if (loginObject == null)
                {
                    return new Response<Login>(false, "Failure: Object doesnot exist.", null);
                }
                _db.Login.Remove(loginObject);
                await _db.SaveChangesAsync();

                return new Response<Login>(true, "Success: Object deleted.", loginObject);
            }
            catch (Exception exception)
            {
                return new Response<Login>(false, "API Failure: Unable to delete object. Because " + exception.Message, null);
            }
        }
    }
}
