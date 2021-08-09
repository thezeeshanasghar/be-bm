using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Nurse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User UserObject { get; set; }
        public int DutyDuration { get; set; }
        public int SharePercentage { get; set; }
        public double Salary { get; set; }
        
    }
    public class Nurses
    {
        public IEnumerable<Nurse> NursesList { get; set; }
        public int Count { get; set; }
    }
}
