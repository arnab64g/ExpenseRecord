using DatabaseLayer.Entities;

namespace ExpenseService.Interface
{
    public interface IExpenseService
    {
        public Task<bool> AddExpenseAsync(Expense expense);
        public Task<List<ExpenseView>?> GetExpenseListAsync(string? UserName);
        public Task<ExpenseView?> GetExpenseViewByIdAsync(int id);
        public Task<bool?> DeleteExpenseById(int id);
        public Task<bool?> UpdateExpenseAsync(Expense expense);
        public Task<List<ExpenseView>?> GetExpenseByDate(DateTimeOffset? FromDate, DateTimeOffset? ToDate, string? UserName);
        public List<ExpenseView> FilterByCategoryAsync(List<ExpenseView> expenseViews, List<string> categoryName);
    }
}
