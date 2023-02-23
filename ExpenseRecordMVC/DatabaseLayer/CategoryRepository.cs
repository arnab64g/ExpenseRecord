using DatabaseLayer.Data;
using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ExpenseDbContext expenseDbContext;

        public CategoryRepository()
        {
            expenseDbContext = new ExpenseDbContext();
        }

        public async Task<List<UserChoice>> GetUserChoiceAsync(string uname)
        {
            var list = await expenseDbContext.UserChoices.Where(d => d.Username == uname).ToListAsync();

            return list;
        }

        public async Task<bool> CreateCategoryAsync(UserChoice userChoice)
        {
            await expenseDbContext.UserChoices.AddAsync(userChoice);
            await expenseDbContext.SaveChangesAsync();

            return true;
        }
    }
}
