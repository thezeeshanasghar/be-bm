using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string BillType { get; set; }
        public string PaymentType { get; set; }
        public string EmployeeOrVender { get; set; }
        public string VoucherNo { get; set; }
        public string ExpenseCategory { get; set; }
        public string EmployeeName { get; set; }
        public double TotalBill { get; set; }
        public string TransactionDetail { get; set; }
    }
    public class Expenses
    {
        public IEnumerable<Expense> expenses { get; set; }
        public int Count { get; set; }
    }
}
