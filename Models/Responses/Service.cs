using System.Collections.Generic;

namespace dotnet.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class Services
    {
        public IEnumerable<Service> services { get; set; }
        public int Count { get; set; }
    }
}
