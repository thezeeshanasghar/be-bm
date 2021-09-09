using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public string Code { get; set; }
        public DateTime Date { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string Type { get; set; }
        public string PatientCategory { get; set; }

        public virtual Patient Patient { get; set; }
    }
    public class Appointments
    {
        public IEnumerable<Appointment> appointments { get; set; }
        public int Count { get; set; }
    }
}

