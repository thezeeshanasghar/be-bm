using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Qualifications
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string Certificate { get; set; }
        public string Description { get; set; }
        public QualificationType qualificationType { get; set; }
    }

    public enum QualificationType
    {
     Cerficate,Deploma

    }
}
