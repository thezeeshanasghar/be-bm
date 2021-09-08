using System.Collections.Generic;

namespace dotnet.Models
{
    public class Procedure
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Executant { get; set; }
        public int Charges { get; set; }
        public int ExecutantShare { get; set; }
        public bool Consent { get; set; }
    }
    public class Procedures
    {
        public IEnumerable<Procedure> Procedure { get; set; }
        public int Count { get; set; }
    }
}
