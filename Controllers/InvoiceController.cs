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
                if (invoiceList != null)
                {
                    if (invoiceList.Count > 0)
                    {
                        return new Response<List<Invoice>>(true, "Success: Acquired data.", invoiceList);
                    }
                }
                return new Response<List<Invoice>>(false, "Failure: Data does not exist.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Invoice>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/id/{id}")]
        public async Task<Response<Invoice>> GetItemById(int id)
        {
            try
            {
                Invoice invoice = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == id);
                if (invoice != null)
                {
                    return new Response<Invoice>(true, "Success: Acquired data.", invoice);
                }
                return new Response<Invoice>(false, "Failure: Data doesnot exist.", null);
            }
            catch (Exception exception)
            {
                return new Response<Invoice>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("search/{search}")]
        public async Task<Response<List<Invoice>>> SearchItems(String search)
        {
            try
            {
                if (String.IsNullOrEmpty(search))
                {
                    return new Response<List<Invoice>>(false, "Failure: Enter a valid search string.", null);
                }
                List<Invoice> invoiceList = await _db.Invoices.Where(x => x.Id.ToString().Contains(search) || x.AppointmentId.ToString().Contains(search) || x.DoctorId.ToString().Contains(search) || x.PatientId.ToString().Contains(search) || x.Date.ToString().Contains(search) || x.CheckupType.Contains(search) || x.CheckupFee.ToString().Contains(search) || x.PaymentType.Contains(search) || x.Disposibles.ToString().Contains(search) || x.GrossAmount.ToString().Contains(search)).OrderBy(x => x.Id).Take(10).ToListAsync();
                if (invoiceList != null)
                {
                    if (invoiceList.Count > 0)
                    {
                        return new Response<List<Invoice>>(true, "Success: Acquired data.", invoiceList);
                    }
                }
                return new Response<List<Invoice>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Invoice>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("post/search")]
        public async Task<Response<List<Invoice>>> SearchItemsByPost(InvoiceSearchRequest request)
        {
            try
            {
                if (request.Search.Length < 1)
                {
                    List<Invoice> invoiceList = await _db.Invoices.Where(x => (x.Date >= request.FromDate.Date && x.Date < request.ToDate.Date.AddDays(1))).
                    Include(x => x.Doctor).Include(x => x.Doctor.User).Include(x => x.Patient).Include(x => x.Patient.User).Include(x => x.Receipt).ToListAsync();
                    if (invoiceList != null)
                    {
                        if (invoiceList.Count > 0)
                        {
                            return new Response<List<Invoice>>(true, "Success: Acquired data.", invoiceList);
                        }
                    }
                    return new Response<List<Invoice>>(false, "Failure: Database is empty.", null);
                }
                else if (request.FromDate != null && request.ToDate != null && request.Search != null)
                {
                    if (request.Search.Length > 0)
                    {
                        List<Invoice> invoiceList = await _db.Invoices.Where(x => (x.Id.ToString() == request.Search) && (x.Date >= request.FromDate.Date &&
                        x.Date < request.ToDate.Date.AddDays(1))).Include(x => x.Doctor).Include(x => x.Doctor.User).
                        Include(x => x.Patient).Include(x => x.Patient.User).Include(x => x.Receipt).ToListAsync();
                        if (invoiceList != null)
                        {
                            if (invoiceList.Count > 0)
                            {
                                return new Response<List<Invoice>>(true, "Success: Acquired data.", invoiceList);
                            }
                        }
                        return new Response<List<Invoice>>(false, "Failure: Database is empty.", null);
                    }
                }
                return new Response<List<Invoice>>(false, "Failure: Any of the following is missing. 'Search' 'FromDate' 'ToDate'", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Invoice>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Invoice>> InsertItem(InvoiceRequest invoiceRequest)
        {
            using var transaction = _db.Database.BeginTransaction();
            int appointmentId = invoiceRequest.AppointmentId;
            DateTime appointmentDate = invoiceRequest.AppointmentDate;
            try
            {
                Patient patient = await _db.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == invoiceRequest.PatientId);
                if (patient == null)
                {
                    transaction.Rollback();
                    return new Response<Invoice>(false, $"Failure: Unable to create invoice. Because patient belonging to id = {invoiceRequest.PatientId} was not found.", null);
                }

                Doctor doctor = await _db.Doctors.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == invoiceRequest.DoctorId);
                if (doctor == null)
                {
                    transaction.Rollback();
                    return new Response<Invoice>(false, $"Failure: Unable to create invoice. Because doctor belonging to id = {invoiceRequest.DoctorId} was not found.", null);
                }

                Appointment appointment = await _db.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentId);
                if (appointment == null)
                {
                    if (invoiceRequest.AppointmentPatientCategory.ToLower() == "online" || invoiceRequest.AppointmentPatientCategory.ToLower() == "admitted")
                    {
                        transaction.Rollback();
                        return new Response<Invoice>(false, $"Failure: Unable to create invoice. Because appointment associated to id = {invoiceRequest.AppointmentId} not found.", null);
                    }
                    Appointment newAppointment = new Appointment();
                    newAppointment.PatientId = invoiceRequest.PatientId;
                    newAppointment.DoctorId = invoiceRequest.DoctorId;
                    newAppointment.ReceptionistId = invoiceRequest.ReceptionistId;
                    newAppointment.Code = invoiceRequest.AppointmentCode;
                    newAppointment.Date = DateTime.UtcNow.Date;
                    newAppointment.ConsultationDate = DateTime.UtcNow.Date;
                    newAppointment.Type = invoiceRequest.AppointmentType;
                    newAppointment.PatientCategory = invoiceRequest.AppointmentPatientCategory;
                    await _db.Appointments.AddAsync(newAppointment);
                    await _db.SaveChangesAsync();

                    appointmentId = newAppointment.Id;
                    appointmentDate = newAppointment.Date;
                }
                else if (appointment.Date.Date != DateTime.UtcNow.Date)
                {
                    Appointment newAppointment = new Appointment();
                    newAppointment.PatientId = invoiceRequest.PatientId;
                    newAppointment.DoctorId = invoiceRequest.DoctorId;
                    newAppointment.ReceptionistId = invoiceRequest.ReceptionistId;
                    newAppointment.Code = invoiceRequest.AppointmentCode;
                    newAppointment.Date = DateTime.UtcNow.Date;
                    newAppointment.ConsultationDate = DateTime.UtcNow.Date;
                    newAppointment.Type = invoiceRequest.AppointmentType;
                    newAppointment.PatientCategory = invoiceRequest.AppointmentPatientCategory;
                    await _db.Appointments.AddAsync(newAppointment);
                    await _db.SaveChangesAsync();

                    appointmentId = newAppointment.Id;
                    appointmentDate = newAppointment.Date;
                }

                Invoice invoice = new Invoice();
                invoice.AppointmentId = appointmentId;
                invoice.DoctorId = invoiceRequest.DoctorId;
                invoice.PatientId = invoiceRequest.PatientId;
                invoice.ReceptionistId = invoiceRequest.ReceptionistId;
                invoice.Date = appointmentDate;
                invoice.CheckupType = invoiceRequest.InvoiceCheckupType;
                invoice.CheckupFee = invoiceRequest.InvoiceCheckupFee;
                invoice.PaymentType = invoiceRequest.InvoicePaymentType;
                invoice.Disposibles = invoiceRequest.InvoiceDisposibles;
                invoice.GrossAmount = invoiceRequest.InvoiceGrossAmount;
                await _db.Invoices.AddAsync(invoice);
                await _db.SaveChangesAsync();

                if (invoiceRequest.ProcedureList != null)
                {
                    if (invoiceRequest.ProcedureList.Count > 0)
                    {
                        foreach (InvoiceProcedureRequest irProcedure in invoiceRequest.ProcedureList)
                        {
                            InvoiceProcedures invoiceProcedures = new InvoiceProcedures();
                            invoiceProcedures.ProcedureId = irProcedure.ProcedureId;
                            invoiceProcedures.InvoiceId = invoice.Id;
                            await _db.InvoiceProcedures.AddAsync(invoiceProcedures);
                            await _db.SaveChangesAsync();
                        }
                    }
                }

                Receipt receipt = new Receipt();
                receipt.PatientId = invoiceRequest.PatientId;
                receipt.ReceiptionistId = invoiceRequest.ReceptionistId;
                receipt.DoctorId = invoiceRequest.DoctorId;
                receipt.InvoiceId = invoice.Id;
                receipt.Pmid = invoiceRequest.ReceiptPmid;
                receipt.Discount = invoiceRequest.ReceiptDiscount;
                receipt.TotalAmount = invoiceRequest.ReceiptTotalAmount;
                receipt.PendingAmount = invoiceRequest.ReceiptPendingAmount;
                receipt.PaidAmount = invoiceRequest.ReceiptPaidAmount;
                await _db.Receipts.AddAsync(receipt);
                await _db.SaveChangesAsync();

                invoice.Doctor = doctor;
                invoice.Patient = patient;

                transaction.Commit();
                return new Response<Invoice>(true, "Success: Created object.", invoice);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                return new Response<Invoice>(false, $"Server Failure: Unable to insert object. Because {exception.Message}", null);
            }
        }
    }
}
