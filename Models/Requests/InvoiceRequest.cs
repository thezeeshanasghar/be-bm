using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class InvoiceRequest
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ReceptionistId { get; set; }

        public DateTime Date { get; set; }
        public string CheckupType { get; set; }
        public int CheckupFee { get; set; }
        public string PaymentType { get; set; }
        public int Disposibles { get; set; }
        public int GrossAmount { get; set; }

        public string Pmid { get; set; }
        public int Discount { get; set; }
        public int TotalAmount { get; set; }
        public int PendingAmount { get; set; }
        public int PaidAmount { get; set; }

        public virtual List<InvoiceProcedureRequest> procedureList { get; set; }
    }
}
