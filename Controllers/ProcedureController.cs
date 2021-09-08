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
    public class ProcedureController : ControllerBase
    {
        private readonly Context _db;

        public ProcedureController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Procedure>>> GetItems()
        {
            try
            {
                List<Procedure> procedureList = await _db.Procedures.ToListAsync();
                if (procedureList != null)
                {
                    if (procedureList.Count > 0)
                    {
                        return new Response<List<Procedure>>(true, "Success: Acquired data.", procedureList);
                    }
                }
                return new Response<List<Procedure>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Procedure>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/id/{id}")]
        public async Task<Response<Procedure>> GetItemById(int id)
        {
            try
            {
                Procedure procedure = await _db.Procedures.FirstOrDefaultAsync(x => x.Id == id);
                if (procedure == null)
                {
                    return new Response<Procedure>(false, "Failure: Data doesn't exist.", null);

                }
                return new Response<Procedure>(true, "Success: Acquired data.", procedure);
            }
            catch (Exception exception)
            {
                return new Response<Procedure>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("search/{search}")]
        public async Task<Response<List<Procedure>>> SearchItems(String search)
        {
            try
            {
                if (String.IsNullOrEmpty(search))
                {
                    return new Response<List<Procedure>>(false, "Failure: Enter a valid search string.", null);
                }
                List<Procedure> procedureList = await _db.Procedures.Where(x => x.Id.ToString().Contains(search) || x.Name.Contains(search) || x.ExecutantShare.ToString().Contains(search) || x.Charges.ToString().Contains(search)).OrderBy(x => x.Id).Take(10).ToListAsync();
                if (procedureList != null)
                {
                    if (procedureList.Count > 0)
                    {
                        return new Response<List<Procedure>>(true, "Success: Acquired data.", procedureList);
                    }
                }
                return new Response<List<Procedure>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Procedure>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Procedure>> InsertItem(ProcedureRequest procedureRequest)
        {
            try
            {
                Procedure procedure = new Procedure();
                procedure.Name = procedureRequest.Name;
                procedure.Executant = procedureRequest.Executant;
                procedure.ExecutantShare = procedureRequest.ExecutantShare;
                procedure.Charges = procedureRequest.Charges;
                procedure.Consent = procedureRequest.Consent;
                await _db.Procedures.AddAsync(procedure);
                await _db.SaveChangesAsync();

                return new Response<Procedure>(true, "Success: Inserted data.", procedure);
            }
            catch (Exception exception)
            {
                return new Response<Procedure>(false, $"Server Failure: Unable to insert data. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Procedure>> UpdateItem(int id, ProcedureRequest procedureRequest)
        {
            try
            {
                if (id != procedureRequest.Id)
                {
                    return new Response<Procedure>(false, "Failure: Id sent in body does not match object Id", null);
                }
                Procedure procedure = await _db.Procedures.FirstOrDefaultAsync(x => x.Id == id);
                if (procedure == null)
                {
                    return new Response<Procedure>(false, "Failure: Data doesn't exist.", null);
                }
                procedure.Name = procedureRequest.Name;
                procedure.Executant = procedureRequest.Executant;
                procedure.ExecutantShare = procedureRequest.ExecutantShare;
                procedure.Charges = procedureRequest.Charges;
                procedure.Consent = procedureRequest.Consent;
                await _db.SaveChangesAsync();

                return new Response<Procedure>(true, "Success: Updated data.", procedure);
            }
            catch (Exception exception)
            {
                return new Response<Procedure>(false, $"Server Failure: Unable to update data. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Procedure>> DeleteItemById(int id)
        {
            try
            {
                Procedure procedure = await _db.Procedures.FindAsync(id);
                if (procedure == null)
                {
                    return new Response<Procedure>(false, "Failure: Object doesn't exist.", null);
                }
                _db.Procedures.Remove(procedure);
                await _db.SaveChangesAsync();

                return new Response<Procedure>(true, "Success: Deleted data.", procedure);
            }
            catch (Exception exception)
            {
                return new Response<Procedure>(false, $"Server Failure: Unable to delete data. Because {exception.Message}", null);
            }
        }
    }
}
