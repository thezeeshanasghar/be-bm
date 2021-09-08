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
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public DateTime Date { get; set; }
        public string CheckupType { get; set; }
        public int CheckupFee { get; set; }
        public string PaymentType { get; set; }
        public int Disposibles { get; set; }
        public int GrossAmount { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
    public class Invoices
    {
        public IEnumerable<Invoice> invoices { get; set; }
        public int Count { get; set; }
    }
}
