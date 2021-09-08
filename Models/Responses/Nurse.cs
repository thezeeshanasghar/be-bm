
using System.Collections.Generic;

namespace dotnet.Models
{
    public class Nurse
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int DutyDuration { get; set; }
        public int SharePercentage { get; set; }
        public int Salary { get; set; }

        public virtual User User { get; set; }

    }
    public class Nurses
    {
        public IEnumerable<Nurse> NursesList { get; set; }
        public int Count { get; set; }
    }
}
