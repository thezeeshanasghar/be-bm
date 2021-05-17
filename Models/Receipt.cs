using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient {get; set;}
        public int DoctorId { get; set; }
        public virtual Doctor Doctor {get; set;}
        public int ReceiptionistId { get; set; }
        public string Pmid { get; set; }
        public long TotalAmount { get; set; }
        public long PendingAmount { get; set; }
        public long PaidAmount { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment{ get; set; }

    } 
}
