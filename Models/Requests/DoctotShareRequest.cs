using System;

namespace dotnet.Models
{
    public class DoctorShareRequest
    {
        public int DoctorId { get; set; }

        public String CheckupType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
