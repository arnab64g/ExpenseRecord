using DatabaseLayer.Data;
using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly ExpenseDbContext expenseDbContext;

        public UserRepository() 
        {
            expenseDbContext = new ExpenseDbContext();
        }

        public async Task CreateUserAsync(UserManager<IdentityUser> userManager, IdentityUser identityUser, string? password)
        {
            await userManager.CreateAsync(identityUser, password);
        }

        public async Task SaveUserDetailsAsync(UserDetails userDetails)
        {
            await expenseDbContext.UserDetails.AddAsync(userDetails);
            await expenseDbContext.SaveChangesAsync();
        }

        public async Task<UserDetails> GetUserDetailsAsync(string? username)
        {
            var result = await expenseDbContext.UserDetails.Where(u => u.Username == username).FirstOrDefaultAsync();
            
            if (result == null)
            {
                return new UserDetails();
            }
            else
            {
                return result;
            }
            
        }

        public async Task<bool> ChangeTotalAmountAsync(string? userName, decimal? Amount)
        {
            var res = await expenseDbContext.UserDetails.Where(x => x.Username == userName).FirstOrDefaultAsync();
            if (res != null)
            {
                res.TotalCost += Amount;
            }
            await expenseDbContext.SaveChangesAsync();

            return true;
        }
    }
}
