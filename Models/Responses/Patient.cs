using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Category { get; set; }
        public string BirthPlace { get; set; }
        public string Type { get; set; }
        public string ExternalId { get; set; }
        public string BloodGroup { get; set; }
        public string ClinicSite { get; set; }
        public string ReferredBy { get; set; }
        public DateTime ReferredDate { get; set; }
        public string Guardian { get; set; }
        public string PaymentProfile { get; set; }
        public string Description { get; set; }
        
        public virtual List<Appointment> Appointments { get; set; }
        public virtual User User { get; set; }
    }
    public class Patients
    {
        public IEnumerable<Patient> PatientsList { get; set; }
        public int Count { get; set; }
    }
}
