using System.Collections.Generic;

namespace dotnet.Models
{
    public class Expense
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

        public virtual User User { get; set; }
    }
    public class Expenses
    {
        public IEnumerable<Expense> expenses { get; set; }
        public int Count { get; set; }
    }
}
