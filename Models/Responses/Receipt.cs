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
        public int PatientId { get; set; }
        public int ReceiptionistId { get; set; }
        public int DoctorId { get; set; }
        public int InvoiceId { get; set; }

        public string Pmid { get; set; }
        public double Discount { get; set; }
        public long TotalAmount { get; set; }
        public long PendingAmount { get; set; }
        public long PaidAmount { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
    public class Receipts
    {
        public IEnumerable<Receipt> receipts { get; set; }
        public int Count { get; set; }
    }
}
