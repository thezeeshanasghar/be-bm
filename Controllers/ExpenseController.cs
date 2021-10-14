using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly Context _db;

        public ExpenseController(Context context)
        {
            _db = context;
        }

        [HttpGet("get")]
        public async Task<Response<List<Expense>>> GetItems()
        {
            try
            {
                List<Expense> expenseList = await _db.Expenses.Include(x => x.User).ToListAsync();
                if (expenseList != null)
                {
                    if (expenseList.Count > 0)
                    {
                        return new Response<List<Expense>>(true, "Success: Acquired data.", expenseList);
                    }
                }
                return new Response<List<Expense>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Expense>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("get/id/{id}")]
        public async Task<Response<Expense>> GetItemById(int id)
        {
            try
            {
                Expense expense = await _db.Expenses.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
                if (expense == null)
                {
                    return new Response<Expense>(false, "Failure: Data doesn't exist.", null);
                }
                return new Response<Expense>(true, "Success: Acquired data.", expense);
            }
            catch (Exception exception)
            {
                return new Response<Expense>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpGet("search/{search}")]
        public async Task<Response<List<Expense>>> SearchItems(String search)
        {
            try
            {
                if (String.IsNullOrEmpty(search))
                {
                    return new Response<List<Expense>>(false, "Failure: Enter a valid search string.", null);
                }
                List<Expense> expenseList = await _db.Expenses.Where(x => x.Id.ToString().Contains(search) || x.UserId.ToString().Contains(search) || x.Name.Contains(search) || x.BillType.Contains(search) || x.PaymentType.Contains(search) || x.EmployeeOrVender.Contains(search) || x.Category.Contains(search) || x.Name.Contains(search) || x.TransactionDetail.Contains(search) || x.VoucherNo.Contains(search)).Include(x => x.User).OrderBy(x => x.Id).Take(10).ToListAsync();
                if (expenseList != null)
                {
                    if (expenseList.Count > 0)
                    {
                        return new Response<List<Expense>>(true, "Success: Acquired data.", expenseList);
                    }
                }
                return new Response<List<Expense>>(false, "Failure: Database is empty.", null);
            }
            catch (Exception exception)
            {
                return new Response<List<Expense>>(false, $"Server Failure: Unable to get data. Because {exception.Message}", null);
            }
        }

        [HttpPost("insert")]
        public async Task<Response<Expense>> InsertItem(ExpenseRequest expenseRequest)
        {
            try
            {
                User user = await _db.Users.FindAsync(expenseRequest.UserId);
                if (user == null)
                {
                    return new Response<Expense>(false, "Failure: User belonging to this id does not exist.", null);
                }
                Expense expense = new Expense();
                expense.UserId = expense.UserId;
                expense.Name = expenseRequest.Name;
                expense.BillType = expenseRequest.BillType;
                expense.PaymentType = expenseRequest.PaymentType;
                expense.EmployeeOrVender = expenseRequest.EmployeeOrVender;
                expense.VoucherNo = expenseRequest.VoucherNo;
                expense.Category = expenseRequest.Category;
                expense.TotalBill = expenseRequest.TotalBill;
                expense.TransactionDetail = expenseRequest.TransactionDetail;
                expense.User = user;
                await _db.Expenses.AddAsync(expense);
                await _db.SaveChangesAsync();

                return new Response<Expense>(true, "Success: Inserted data.", expense);
            }
            catch (Exception exception)
            {
                return new Response<Expense>(false, $"Server Failure: Unable to insert data. Because {exception.Message}", null);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<Response<Expense>> UpdateItem(int id, ExpenseRequest expenseRequest)
        {
            try
            {
                if (id != expenseRequest.Id)
                {
                    return new Response<Expense>(false, "Failure: Id sent in body does not match object Id", null);
                }
                Expense expense = await _db.Expenses.FirstOrDefaultAsync(x => x.Id == id); ;
                if (expense == null)
                {
                    return new Response<Expense>(false, $"Failure: Unable to update expense. Because Id is invalid. ", null);
                }
                expense.UserId = expense.UserId;
                expense.Name = expenseRequest.Name;
                expense.BillType = expense.BillType;
                expense.PaymentType = expenseRequest.PaymentType;
                expense.EmployeeOrVender = expenseRequest.EmployeeOrVender;
                expense.VoucherNo = expenseRequest.VoucherNo;
                expense.Category = expenseRequest.Category;
                expense.TotalBill = expenseRequest.TotalBill;
                expense.TransactionDetail = expenseRequest.TransactionDetail;
                await _db.SaveChangesAsync();

                return new Response<Expense>(true, "Success: Updated data.", expense);
            }
            catch (Exception exception)
            {
                return new Response<Expense>(false, $"Server Failure: Unable to update data. Because {exception.Message}", null);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<Response<Expense>> DeleteItemById(int id)
        {
            try
            {
                Expense expense = await _db.Expenses.FindAsync(id);
                if (expense == null)
                {
                    return new Response<Expense>(false, "Failure: Object doesn't exist.", null);
                }
                _db.Expenses.Remove(expense);
                await _db.SaveChangesAsync();

                return new Response<Expense>(true, "Success: Deleted data.", expense);
            }
            catch (Exception exception)
            {
                return new Response<Expense>(false, $"Server Failure: Unable to delete data. Because {exception.Message}", null);
            }
        }
    }
}
