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
        [HttpGet]
         public async Task<Response<List<Room>>> GetAll(string? key)
        {
             List<Room> rooms;
            if (key != "" && key != null)
            {
                rooms = await _db.rooms.Where(x => x.RoomNo.ToLower().Contains(key) || x.RoomType.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                 rooms =  await _db.rooms.ToListAsync();
            }
             return new Response<List<Room>>(true, "Successfully", rooms);
        }

        // GET api/Room/5
        [HttpGet("{id}")]
        public async Task<Response<Room>> GetSingle(long id)
        {
            var Room = await _db.rooms.FirstOrDefaultAsync(x => x.Id == id);
            if (Room == null)
                return new Response<Room>(false, "Record not found", null);

             return new Response<Room>(true, "operation succcessful", Room);
        }

        // POST api/Room
       [HttpPost]
        public async Task<ActionResult<Room>> Post(Room Room)
        {
            _db.rooms.Update(Room);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Room.Id }, Room);
        }

        // PUT api/Room/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Room Room)
        {
            if (id != Room.Id)
                return BadRequest();
            _db.Entry(Room).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Room = await _db.rooms.FindAsync(id);

            if (Room == null)
                return NotFound();

            _db.rooms.Remove(Room);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
