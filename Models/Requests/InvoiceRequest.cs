using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class InvoiceRequest
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int ReceptionistId { get; set; }
        public int AppointmentId { get; set; }

        public DateTime InvoiceDate { get; set; }
        public string InvoiceCheckupType { get; set; }
        public int InvoiceCheckupFee { get; set; }
        public string InvoicePaymentType { get; set; }
        public int InvoiceDisposibles { get; set; }
        public int InvoiceGrossAmount { get; set; }

        public string ReceiptPmid { get; set; }
        public int ReceiptDiscount { get; set; }
        public int ReceiptTotalAmount { get; set; }
        public int ReceiptPendingAmount { get; set; }
        public int ReceiptPaidAmount { get; set; }

        public string AppointmentCode { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentConsultationDate { get; set; }
        public String AppointmentType { get; set; }
        public String AppointmentPatientCategory { get; set; }

        public bool AppointmentDetailsHasDischarged { get; set; }
        public String AppointmentDetailsWalkinType { get; set; }

        public List<InvoiceProcedureRequest> ProcedureList { get; set; }
    }
}
