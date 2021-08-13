using System.Collections.Generic;

namespace dotnet.Models
{
    public class Receptionist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string JobType { get; set; }
        public string ShiftTime { get; set; }
        public virtual User User { get; set; }
    }

    public class Receptionists
    {
        public IEnumerable<Receptionist> ReceptionistList { get; set; }
        public int Count { get; set; }
    }
}
