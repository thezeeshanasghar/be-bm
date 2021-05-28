using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public DateTime PreviousVisitDate { get; set; }
        public DateTime TodayVisitDate { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
       
        public string CheckupType { get; set; }
        public string PaymentType { get; set; }
        public int ProcedureId { get; set; }
        public virtual Procedure Procedures { get; set; }
        public double  ConsultationFee { get; set; }
        public double Discount { get; set; }
        public double NetAmount { get; set; }
        public double Disposibles { get; set; }
        public double GrossAmount { get; set; }
        public int IsRefund { get; set; }
        public double RefundAmount { get; set; }
    }
    public class Invoices
    {
        public IEnumerable<Invoice> invoices { get; set; }
        public int Count { get; set; }
    }
}
