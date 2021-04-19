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
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string FatherHusbandName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string LocalArea { get; set; }
        public DateTime Dob { get; set; }
        public string Contact { get; set; }
        public string PatientDetails { get; set; }
    } 
}
