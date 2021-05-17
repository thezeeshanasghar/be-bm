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
    public class ExpenseController : ControllerBase
    {

          private readonly Context _db;

        public ExpenseController(Context context)
        {
            _db = context;
        }

        // GET api/Expense
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Expense>>> GetAll()
        {
            return await _db.expenses.ToListAsync();
        }

        // GET api/Expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetSingle(long id)
        {
            var Expense = await _db.expenses.FirstOrDefaultAsync(x => x.Id == id);
            if (Expense == null)
                return NotFound();

            return Expense;
        }

        // POST api/Expense
       [HttpPost]
        public async Task<ActionResult<Expense>> Post(Expense Expense)
        {
            _db.expenses.Update(Expense);
            
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingle), new { id = Expense.Id }, Expense);
        }

        // PUT api/Expense/5
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Expense Expense)
        {
            if (id != Expense.Id)
                return BadRequest();
            _db.Entry(Expense).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/Expense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Expense = await _db.expenses.FindAsync(id);

            if (Expense == null)
                return NotFound();

            _db.expenses.Remove(Expense);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
