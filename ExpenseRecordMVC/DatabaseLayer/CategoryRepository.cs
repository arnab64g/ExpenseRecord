using DatabaseLayer.Data;
using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UserDbContext userDbContext;

        public CategoryRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<UserChoice>> GetUserChoiceAsync(string? uname)
        {
            var list = await userDbContext.UserChoices.Where(d => d.Username == uname).ToListAsync();

            return list;
        }

        public async Task<bool> CreateCategoryAsync(UserChoice userChoice)
        {
            var res = await userDbContext.UserChoices.AnyAsync(d => d.Username== userChoice.Username && 
                d.CategoryName == userChoice.Username);

            if (res)
            {
                return false;
            }
            
            await userDbContext.UserChoices.AddAsync(userChoice);
            await userDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserChoice?> UserChoiceByIdAsync(int id)
        {
            var res = await userDbContext.UserChoices.Where(d => d.Id== id).FirstOrDefaultAsync();

            return res;
        }

        public async Task<bool?> SaveChoiceAsync(int id, string? newName)
        {
            var data = await userDbContext.UserChoices.Where(d => d.Id == id).FirstOrDefaultAsync();
            
            if(data != null) 
            {
                data.CategoryName = newName;
            }
            
            await userDbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool?> DeleteChoiceAsync(int id)
        {
            var item = await userDbContext.UserChoices.Where(d => d.Id == id).FirstOrDefaultAsync();
            if(item != null)
            {
                userDbContext.UserChoices.Remove(item);
                await userDbContext.SaveChangesAsync() ;

                return true;
            }
            else
            {
                return true;
            }
        }

        public async Task<int> GerUserChoiceIdAsync(string? userName, string? CategoryName)
        {
            var id = await userDbContext.UserChoices.Where(d => d.Username==userName && d.CategoryName == CategoryName)
                .Select(d => d.Id).FirstOrDefaultAsync();

            return id;
        }
    }
}
