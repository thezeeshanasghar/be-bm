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
    public class RoomController : ControllerBase
    {

        private readonly Context _db;

        public RoomController(Context context)
        {
            _db = context;
        }

        // GET api/Room
        [HttpGet("get")]
        public async Task<Response<List<Room>>> GetAll(string key)
        {
            List<Room> RoomList;
            if (key != "" && key != null)
            {
                RoomList = await _db.Rooms.Where(x => x.RoomNo.ToLower().Contains(key) || x.RoomType.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                RoomList = await _db.Rooms.ToListAsync();
            }
            return new Response<List<Room>>(true, "Successfully", RoomList);
        }

        // GET api/Room/5
        [HttpGet("get/{id}")]
        public async Task<Response<Room>> GetSingle(long id)
        {
            var Room = await _db.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (Room == null)
                return new Response<Room>(false, "Record not found", null);

            return new Response<Room>(true, "operation succcessful", Room);
        }

        // POST api/Room
        [HttpPost("insert")]
        public async Task<ActionResult<Room>> Post(Room Room)
        {
            _db.Rooms.Update(Room);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Room.Id }, Room);
        }

        // PUT api/Room/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(long id, Room Room)
        {
            if (id != Room.Id)
                return BadRequest();
            _db.Entry(Room).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Room/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Room = await _db.Rooms.FindAsync(id);

            if (Room == null)
                return NotFound();

            _db.Rooms.Remove(Room);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
