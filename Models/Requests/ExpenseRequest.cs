namespace dotnet.Models
{
    public class ExpenseRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Name { get; set; }
        public string BillType { get; set; }
        public string PaymentType { get; set; }
        public string EmployeeOrVender { get; set; }
        public string VoucherNo { get; set; }
        public string Category { get; set; }
        public double TotalBill { get; set; }
        public string TransactionDetail { get; set; }
    }
}
