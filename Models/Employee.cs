using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string   EmployeeType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherHusbandName { get; set; }
        public string Gender { get; set; }
        public string CNIC { get; set; }
        public string Contact { get; set; }
        public string EmergencyContact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int FlourNo { get; set; }
        public string Experience { get; set; }
        public virtual List<Qualifications> Qualifications { get; set; }

    }
    public class Employees {
        public IEnumerable<Employee> employees { get; set; }
        public int Count { get; set; }
    }
}
