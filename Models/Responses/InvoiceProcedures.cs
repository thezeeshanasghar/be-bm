using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class InvoiceProcedures
    {
        public int Id { get; set; }
        public virtual Procedure Procedures { get; set; }
        public int ProcedureId { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
