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
    public class ServiceController : ControllerBase
    {
        private readonly Context _db;

        public ServiceController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Service>>> GetItems()
        {
            try
            {
                List<Service> serviceList = await _db.Services.ToListAsync();
                if (serviceList != null)
                {
                    if (serviceList.Count > 0)
                    {
                        return new Response<List<Service>>(true, "Success: Acquired data.", serviceList);
                    }
                }
                return new Response<List<Service>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Service>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/id/{id}")]
        public async Task<Response<Service>> GetItemById(int id)
        {
            try
            {
                Service service = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
                if (service == null)
                {
                    return new Response<Service>(false, "Failure: Data doesn't exist.", null);
                }
                return new Response<Service>(true, "Success: Acquired data.", service);
            }
            catch (Exception exception)
            {
                return new Response<Service>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("search/{search}")]
        public async Task<Response<List<Service>>> SearchItems(String search)
        {
            try
            {
                if (String.IsNullOrEmpty(search))
                {
                    return new Response<List<Service>>(false, "Failure: Enter a valid search string.", null);
                }
                List<Service> serviceList = await _db.Services.Where(x => x.Id.ToString().Contains(search) || x.Name.Contains(search) || x.Description.Contains(search)).OrderBy(x => x.Id).Take(10).ToListAsync();
                if (serviceList != null)
                {
                    if (serviceList.Count > 0)
                    {
                        return new Response<List<Service>>(true, "Success: Acquired data.", serviceList);
                    }
                }
                return new Response<List<Service>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Service>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Service>> InsertItem(ServiceRequest serviceRequest)
        {
            try
            {
                Service service = new Service();
                service.Name = serviceRequest.Name;
                service.Description = serviceRequest.Description;
                await _db.Services.AddAsync(service);
                await _db.SaveChangesAsync();

                return new Response<Service>(true, "Success: Inserted data.", service);
            }
            catch (Exception exception)
            {
                return new Response<Service>(false, $"Server Failure: Unable to insert data. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Service>> UpdateItem(int id, ServiceRequest serviceRequest)
        {
            try
            {
                if (id != serviceRequest.Id)
                {
                    return new Response<Service>(false, "Failure: Id sent in body does not match object Id", null);
                }
                Service service = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
                if (service == null)
                {
                    return new Response<Service>(false, "Failure: Data doesn't exist.", null);
                }
                service.Name = serviceRequest.Name;
                service.Description = serviceRequest.Description;
                await _db.SaveChangesAsync();

                return new Response<Service>(true, "Success: Updated data.", service);
            }
            catch (Exception exception)
            {
                return new Response<Service>(false, $"Server Failure: Unable to update data. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Service>> DeleteItemById(int id)
        {
            try
            {
                Service service = await _db.Services.FindAsync(id);
                if (service == null)
                {
                    return new Response<Service>(false, "Failure: Object doesn't exist.", null);
                }
                _db.Services.Remove(service);
                await _db.SaveChangesAsync();

                return new Response<Service>(true, "Success: Deleted data.", service);
            }
            catch (Exception exception)
            {
                return new Response<Service>(false, $"Server Failure: Unable to delete data. Because {exception.Message}", null);
            }
        }
    }
}
