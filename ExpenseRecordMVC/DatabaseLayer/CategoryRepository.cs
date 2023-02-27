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

        public async Task<List<UserChoice>> GetUserChoiceAsync(string? uname)
        {
            var list = await expenseDbContext.UserChoices.Where(d => d.Username == uname).ToListAsync();

            return list;
        }

        public async Task<bool> CreateCategoryAsync(UserChoice userChoice)
        {
            var res = await expenseDbContext.UserChoices.AnyAsync(d => d.Username== userChoice.Username && 
                d.CategoryName == userChoice.Username);

            if (res)
            {
                return false;
            }
            
            await expenseDbContext.UserChoices.AddAsync(userChoice);
            await expenseDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserChoice?> UserChoiceByIdAsync(int id)
        {
            var res = await expenseDbContext.UserChoices.Where(d => d.Id== id).FirstOrDefaultAsync();

            return res;
        }

        public async Task<bool?> SaveChoiceAsync(int id, string? newName)
        {
            var data = await expenseDbContext.UserChoices.Where(d => d.Id == id).FirstOrDefaultAsync();
            
            if(data != null) 
            {
                data.CategoryName = newName;
            }
            
            await expenseDbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool?> DeleteChoiceAsync(int id)
        {
            var item = await expenseDbContext.UserChoices.Where(d => d.Id == id).FirstOrDefaultAsync();
            if(item != null)
            {
                expenseDbContext.UserChoices.Remove(item);
                await expenseDbContext.SaveChangesAsync() ;

                return true;
            }
            else
            {
                return true;
            }
        }

        public async Task<int> GerUserChoiceIdAsync(string? userName, string? CategoryName)
        {
            var id = await expenseDbContext.UserChoices.Where(d => d.Username==userName && d.CategoryName == CategoryName)
                .Select(d => d.Id).FirstOrDefaultAsync();

            return id;
        }
    }
}
