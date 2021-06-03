using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientCategory { get; set; }
        public string Name { get; set; }
        public string FatherHusbandName { get; set; }
        public DateTime Dob { get; set; }
        public string Sex { get; set; }
        public string PlaceofBirth {get;set;}
        public string Email { get; set; }
        public string Contact { get; set; }
        public string cnic { get; set; }
        public string MaritalStatus { get; set; }
       public string PatientType { get; set; }
        public string ExternalId { get; set; }
        public string BloodGroup { get; set; }
        public string ClinicSite { get; set; }
        public string ReferedBy { get; set; }
        public DateTime ReferedDate { get; set; }
        public string Religion { get; set; }
        public string PatientGardian { get; set; }
        public string PaymentProfile { get; set; }
        public string City { get; set; }
        public string LocalArea { get; set; }
        public string PatientDetails { get; set; }
    }
    public class Patients
    {
        public IEnumerable<Patient> patients { get; set; }
        public int Count { get; set; }
    }

}
