using System.Collections.Generic;

namespace dotnet.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int ConsultationFee { get; set; }
        public int EmergencyConsultationFee { get; set; }
        public int ShareInFee { get; set; }
        public string SpecialityType { get; set; }

        public virtual User User { get; set; }
    }

    public class Doctors
    {
        public IEnumerable<Doctor> DoctorsList { get; set; }
        public int Count { get; set; }
    }
}
