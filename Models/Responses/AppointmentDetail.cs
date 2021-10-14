using System;
using System.Collections.Generic;

namespace dotnet.Models
{
    public class AppointmentDetail
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public bool HasDischarged { get; set; }
        public String WalkinType { get; set; }
    }
    public class AppointmentDetails
    {
        public IEnumerable<AppointmentDetail> appointmentDetails { get; set; }
        public int Count { get; set; }
    }
}

