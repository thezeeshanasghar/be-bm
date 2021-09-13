using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet.Models
{
    public class AppointmentRequest
    {
        public string Search { get; set; }
        public string Category { get; set; }
        public string Doctor { get; set; }
        public DateTime Date { get; set; }
        public string Booked { get; set; }

        public string searchFrom { get; set; }
    }
}
