using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dotnet.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee employee { get; set; }
        public int ConsultationFee { get; set; }
        public int EmergencyConsultationFee { get; set; }
        public int ShareInFee { get; set; }
        public string SpecialityType {get ; set;}
    }
    public class Doctors
    {
        public IEnumerable<Doctor> doctors { get; set; }
        public int Count { get; set; }
    }
}
