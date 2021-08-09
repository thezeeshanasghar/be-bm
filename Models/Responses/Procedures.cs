using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Procedure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PerformedBy { get; set; }
        public int Charges { get; set; }
        public int PerformerShare { get; set; }
    }
    public class Procedures
    {
        public  IEnumerable<Procedure>  Procedure { get;set;}
        public int Count { get; set; } 
    }
}
