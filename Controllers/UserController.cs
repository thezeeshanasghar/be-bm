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
    public class UserController : ControllerBase
    {

        //     private readonly Context _db;

        //     public UserController(Context context)
        //     {
        //         _db = context;
        //     }

        //     // GET api/User
        //     [HttpGet("get")]
        //     public async Task<Response<List<User>>> GetAll(String key)
        //     {
        //         List<User> UserList;
        //         if (key != "" && key != null)
        //         {
        //             UserList = await _db.Users.Where(x => x.FirstName.ToLower().Contains(key) || x.LastName.ToLower().Contains(key) || x.Email.ToLower().Contains(key) || x.Contact.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
        //         }
        //         else
        //         {
        //             UserList = await _db.Users.ToListAsync();
        //         }
        //         return new Response<List<User>>(true, "Operation Successful", UserList);
        //     }

        //     // GET api/User/5
        //     [HttpGet("get/{id}")]
        //     public async Task<Response<User>> GetSingle(long id)
        //     {
        //         var User = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        //         if (User == null)
        //             return new Response<User>(false, "Record not found", null);
        //         return new Response<User>(true, "Operation Successful", User);
        //     }

        //     // POST api/User
        //     [HttpPost("insert")]
        //     public async Task<ActionResult<User>> Post(User User)
        //     {
        //         _db.Users.Update(User);

        //         await _db.SaveChangesAsync();

        //         return CreatedAtAction(nameof(GetSingle), new { id = User.Id }, User);
        //     }

        //     // PUT api/User/5
        //     [HttpPut("update/{id}")]
        //     public async Task<IActionResult> Put(long id, User User)
        //     {
        //         if (id != User.Id)
        //             return BadRequest();
        //         _db.Entry(User).State = EntityState.Modified;
        //         await _db.SaveChangesAsync();

        //         return NoContent();
        //     }

        //     // DELETE api/User/5
        //     [HttpDelete("delete/{id}")]
        //     public async Task<IActionResult> Delete(int id)
        //     {
        //         var User = await _db.Users.FindAsync(id);

        //         if (User == null)
        //             return NotFound();

        //         _db.Users.Remove(User);
        //         await _db.SaveChangesAsync();
        //         return NoContent();
        //     }
    }
}
