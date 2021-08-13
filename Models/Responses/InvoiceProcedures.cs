namespace dotnet.Models
{
    public class InvoiceProcedures
    {
        public int Id { get; set; }
        public int ProcedureId { get; set; }
        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Procedure Procedures { get; set; }
    }
}
