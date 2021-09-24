using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet.Models
{
    public class AddAppointmentRequest
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ReceptionistId { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string Type { get; set; }
        public string PatientCategory { get; set; }
    }
}
