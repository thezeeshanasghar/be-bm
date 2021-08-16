using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherHusbandName { get; set; }
        public string Gender { get; set; }
        public string Cnic { get; set; }
        public string Contact { get; set; }
        public string EmergencyContact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public int FloorNo { get; set; }
        public string Experience { get; set; }

        public virtual List<Qualification> Qualifications { get; set; }
    }
    public class Users
    {
        public IEnumerable<User> UserList { get; set; }
        public int Count { get; set; }
    }
}
