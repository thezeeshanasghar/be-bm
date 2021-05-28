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
    public class ServiceController : ControllerBase
    {

          private readonly Context _db;

        public ServiceController(Context context)
        {
            _db = context;
        }

        // GET api/Service
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Service>>> GetAll(string key)
        {
            if (key != "" && key != null)
            {
                return await _db.services.Where(x => x.Name.ToLower().Contains(key) || x.Description.ToLower().Contains(key) || x.Id.ToString().Contains(key)).ToListAsync();
            }
            else
            {
                return await _db.services.ToListAsync();
            }
        }

        // GET api/Service/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetSingle(long id)
        {
            var Service = await _db.services.FirstOrDefaultAsync(x => x.Id == id);
            if (Service == null)
                return NotFound();

            return Service;
        }

        // POST api/Service
       [HttpPost]
        public async Task<ActionResult<Service>> Post(Service Service)
        {
            _db.services.Update(Service);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Service.Id }, Service);
        }

        // PUT api/Service/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Service Service)
        {
            if (id != Service.Id)
                return BadRequest();
            _db.Entry(Service).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Service = await _db.services.FindAsync(id);

            if (Service == null)
                return NotFound();

            _db.services.Remove(Service);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
