using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using ExpenseService.Interface;

namespace ExpenseService
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IUserRepository userRepository;

        public ExpenseService(IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            this.expenseRepository = expenseRepository;
            this.userRepository = userRepository;
        }

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            var res1 = await expenseRepository.AddExpenseAsync(expense);
            var res2 = await userRepository.ChangeTotalAmountAsync(expense.Username, expense.Amount);

            return res1 && res2;
        }

        public async Task<bool?> DeleteExpenseById(int id)
        {
            var exp = await expenseRepository.GetExpenseById(id);

            await userRepository.ChangeTotalAmountAsync(exp.Username, - 1 * exp.Amount);
            await expenseRepository.DeleteExpenseAsync(exp);

            return true;
        }

        public async Task<List<ExpenseView>?> GetExpenseListAsync(string? UserName)
        {
            return await expenseRepository.GetExpenseListAsync(UserName);
        }

        public async Task<ExpenseView?> GetExpenseViewByIdAsync(int id)
        {
            return await expenseRepository.GetExpenseViewByIdAsync(id);
        }

        public async Task<bool?> UpdateExpenseAsync(Expense expense)
        {
            var amount = await expenseRepository.GetAmountByIdAsync(expense.Id);
            decimal? differ = expense.Amount - amount;
            
            await userRepository.ChangeTotalAmountAsync(expense.Username, differ);
            var res = await expenseRepository.UpdateExpenseAsync(expense);

            return res;
        }
    }
}
