using System.Text.Json.Serialization;

namespace dotnet.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string EmergencyContact { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public int ConsultationFee { get; set; }
        public int EmergencyConsultationFee { get; set; }
        public int ShareInFee { get; set; }
        public SpecialityType SpecialityType {get ; set;}
    }


      public enum SpecialityType
    {
        Cardiologist,
        Dentist,
        Ent_Surgeon,
        Eye_Specialist,
        Eye_Surgeon,
        Gastroenterologist,
        General_Physician,
        General_Surgeon,
        Gynecologist,
        Nephrologist,
        Neuro_Surgeon,
        Neurologist, 
        Orthopedic_Surgeon,
        Pediatric_Surgeon, 
        Pediatrician, 
        Physiotherapist, 
        Plastic_Surgeon, 
        Psychiatrist, 
        Psychologist, 
        Radiologist, 
        Urologist,

    }

}
