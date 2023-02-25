using DatabaseLayer.Entities;

namespace DatabaseLayer.Interface
{
    public interface IExpenseRepository
    {
        public Task<bool> AddExpenseAsync(Expense expense);
        public Task<List<ExpenseView>?> GetExpenseListAsync(string? UserName);
        public Task<bool> IsUsedCategory(int id);
        public Task<ExpenseView?> GetExpenseViewByIdAsync(int id);
        public Task<Expense?> GetExpenseById(int id);
        public Task<bool?> DeleteExpenseAsync(Expense expense);
        public Task<decimal?> GetAmountByIdAsync(int id);
        public Task<bool?> UpdateExpenseAsync(Expense expense);
        public Task<List<ExpenseView>?> GetExpenseByDate(DateTimeOffset? FromDate, DateTimeOffset? ToDate, string? UserName);
    }
}
