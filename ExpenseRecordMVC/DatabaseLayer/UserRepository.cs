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

        public async Task<DateTimeOffset?> UserCreatedDateAsync(string? userName)
        {
            var userCreatedDate = await expenseDbContext.UserDetails.Where(u => u.Username == userName)
                .Select(d => d.Created).FirstOrDefaultAsync();

            return userCreatedDate;
        }

        public async Task<List<UserListData>> GetAllUserAsync()
        {
            var allUsers = await expenseDbContext.UserDetails.Select(d => new UserListData
            {
                Id = d.Id,
                Name = d.Name,
                Username = d.Username,
                Photo = d.Photo
            }).ToListAsync();

            return allUsers;
        }

        public async Task<UserDetailsCore?> GetUserDetailsByIdAsync(int id)
        {
            var user = await expenseDbContext.UserDetails.Select(d => new UserDetailsCore
            {
                Id = d.Id,
                Name = d.Name,
                Username = d.Username,
                Photo = d.Photo,
                Email = d.Email,
                Address = d.Address,
                Created = d.Created
            }).FirstOrDefaultAsync();

            return user;
        }
    }
}
