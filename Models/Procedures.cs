using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Procedures
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PerformedBy { get; set; }
        public int Charges { get; set; }
        public int PerformerShare { get; set; }
    }
}
