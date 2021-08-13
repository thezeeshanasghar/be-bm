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
    public class InvoiceController : ControllerBase
    {
        private readonly Context _db;

        public InvoiceController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Invoice>>> GetItems()
        {
            try
            {
                List<Invoice> invoiceList = await _db.Invoices.ToListAsync();
                if (invoiceList != null && invoiceList.Count > 0)
                {
                    return new Response<List<Invoice>>(true, "Success: Acquired data.", invoiceList);
                }
                else
                {
                    return new Response<List<Invoice>>(false, "Failure: Data does not exist.", null);
                }
            }
            catch (Exception exception)
            {
                return new Response<List<Invoice>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<Response<Invoice>> GetItemById(long id)
        {
            try
            {
                Invoice invoice = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == id);
                if (invoice != null)
                {
                    return new Response<Invoice>(true, "Success: Acquired data.", invoice);
                }
                else
                {
                    return new Response<Invoice>(false, "Failure: Data doesnot exist.", null);
                }
            }
            catch (Exception exception)
            {
                return new Response<Invoice>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Invoice>> InsertItem(InvoiceRequest invoiceRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                Invoice invoice = new Invoice();
                invoice.AppointmentId = invoiceRequest.AppointmentId;
                invoice.DoctorId = invoiceRequest.DoctorId;
                invoice.PatientId = invoiceRequest.PatientId;
                invoice.Date = invoiceRequest.Date;
                invoice.CheckupType = invoiceRequest.CheckupType;
                invoice.CheckupFee = invoiceRequest.CheckupFee;
                invoice.PaymentType = invoiceRequest.PaymentType;
                invoice.Disposibles = invoiceRequest.Disposibles;
                invoice.Disposibles = invoiceRequest.GrossAmount;
                await _db.Invoices.AddAsync(invoice);
                await _db.SaveChangesAsync();

                foreach (InvoiceProcedureRequest irProcedure in invoiceRequest.procedureList)
                {
                    InvoiceProcedures invoiceProcedures = new InvoiceProcedures();
                    invoiceProcedures.ProcedureId = irProcedure.InvoiceId;
                    invoiceProcedures.InvoiceId = invoice.Id;
                    await _db.InvoiceProcedures.AddAsync(invoiceProcedures);
                    await _db.SaveChangesAsync();
                }

                Receipt receipt = new Receipt();
                receipt.PatientId = invoiceRequest.PatientId;
                receipt.ReceiptionistId = invoiceRequest.ReceptionistId;
                receipt.DoctorId = invoiceRequest.DoctorId;
                receipt.Pmid = invoiceRequest.Pmid;
                receipt.Discount = invoiceRequest.Discount;
                receipt.TotalAmount = invoiceRequest.TotalAmount;
                receipt.PendingAmount = invoiceRequest.PendingAmount;
                receipt.PaidAmount = invoiceRequest.PaidAmount;
                await _db.Receipts.AddAsync(receipt);
                await _db.SaveChangesAsync();

                transaction.Commit();
                return new Response<Invoice>(true, "Success: Created object.", invoice);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Invoice>(false, $"Server Failure: Unable to insert object. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Invoice>> DeleteItemById(long id)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    return new Response<Invoice>(false, "Failure: Object doesnot exist.", null);
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return new Response<Invoice>(true, "Success: Object deleted.", null);
            }
            catch (Exception exception)
            {
                return new Response<Invoice>(false, $"Server Failure: Unable to delete object. Because {exception.Message}", null);
            }
        }

    }
}
