using DatabaseLayer.Data;
using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseDbContext dbContext;

        public ExpenseRepository() 
        {
            dbContext = new ExpenseDbContext();
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            await dbContext.Expenses.AddAsync(expense);
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ExpenseView>?> GetExpenseListAsync(string? UserName)
        {
            var list = await (from expense in dbContext.Expenses.Where(d => d.Username == UserName)
                              join uc in dbContext.UserChoices.Where(d => d.Username == UserName)
                              on expense.CategoryId equals uc.Id
                              select new ExpenseView
                              {
                                  Id = expense.Id,
                                  CategoryName = uc.CategoryName,
                                  Amount = expense.Amount,
                                  Date = expense.Date,
                                  Description = expense.Discription
                              }).OrderBy(d => d.Date).ToListAsync();
            
            return list;              
        }

        public async Task<bool> IsUsedCategory(int id)
        {
            var res = await dbContext.Expenses.AnyAsync(d => d.CategoryId == id);

            if (res)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ExpenseView?> GetExpenseViewByIdAsync(int id)
        {
            var expense = await dbContext.Expenses.Where(exp => exp.Id == id).FirstOrDefaultAsync();
            var CatName = await dbContext.UserChoices.Where(cat => cat.Id == expense.CategoryId)
                .FirstOrDefaultAsync();

            var expenseView = new ExpenseView
            {
                Id = expense.Id,
                CategoryName = CatName.CategoryName,
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Discription
            };

            return expenseView;
        }

        public async Task<Expense?> GetExpenseById(int id)
        {
            var exp = await dbContext.Expenses.Where(d => d.Id == id).FirstOrDefaultAsync();

            return exp;
        }

        public async Task<bool?> DeleteExpenseAsync(Expense expense)
        {
            dbContext.Expenses.Remove(expense);
            await dbContext.SaveChangesAsync(); 

            return true;
        }

        public async Task<decimal?> GetAmountByIdAsync(int id)
        {
            var amount = await dbContext.Expenses.Where(e => e.Id == id).Select(e => e.Amount).FirstOrDefaultAsync();

            return amount;
        }

        public async Task<bool?> UpdateExpenseAsync(Expense expense)
        {
            var OldExpense = await dbContext.Expenses.Where(e => e.Id == expense.Id).FirstOrDefaultAsync();
            
            if (OldExpense != null)
            {
                OldExpense.Amount = expense.Amount;
                OldExpense.Date = expense.Date;
                OldExpense.CategoryId= expense.CategoryId;

                await dbContext.SaveChangesAsync(true);
            }

            return true;
        }

        public async Task<List<ExpenseView>?> GetExpenseByDate(DateTimeOffset? FromDate, DateTimeOffset? ToDate, string? UserName)
        {
            var list = await (from expense in dbContext.Expenses.Where(d => d.Username == UserName && d.Date >= FromDate && d.Date <= ToDate)
                             join uc in dbContext.UserChoices.Where(d => d.Username == UserName)
                             on expense.CategoryId equals uc.Id
                             select new ExpenseView
                             {
                                 Id = expense.Id,
                                 CategoryName = uc.CategoryName,
                                 Amount = expense.Amount,
                                 Date = expense.Date
                             }).OrderBy(d => d.Date).ToListAsync();

            return list;

        }
    }
}
