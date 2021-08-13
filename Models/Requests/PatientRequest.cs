using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class PatientRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherHusbandName { get; set; }
        public string Gender { get; set; }
        public string Cnic { get; set; }
        public string Contact { get; set; }
        public string EmergencyContact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public int FloorNo { get; set; }
        public string Experience { get; set; }

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

        public string AppointmentCode { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string AppointmentType { get; set; }
    }
}
