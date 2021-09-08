using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Refund
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }

        public int RefundAmount { get; set; }
        public int FinalAmount { get; set; }

        public virtual Receipt Receipt { get; set; }
    }
}
