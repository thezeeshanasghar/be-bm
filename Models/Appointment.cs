using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public string AppointmentCode { get; set; }
        public DateTime AppointmentDate {get; set;}
        public string AppointmentType { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

    }
    public class Appointments
    {
        public IEnumerable<Appointment> appointments { get; set; }
        public int Count { get; set; }
    }

}

