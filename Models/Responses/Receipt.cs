using System.Collections.Generic;

namespace dotnet.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ReceiptionistId { get; set; }
        public int DoctorId { get; set; }
        public int InvoiceId { get; set; }

        public int DoctorFee { get; set; }
        public string Pmid { get; set; }
        public int Discount { get; set; }
        public int TotalAmount { get; set; }
        public int PendingAmount { get; set; }
        public int PaidAmount { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
    public class Receipts
    {
        public IEnumerable<Receipt> receipts { get; set; }
        public int Count { get; set; }
    }
}
