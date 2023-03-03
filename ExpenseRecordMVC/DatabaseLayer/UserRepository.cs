using DatabaseLayer.Data;
using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using ExtraLibraries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext userDbContext;

        public UserRepository(UserDbContext userDbContext) 
        {
            this.userDbContext = userDbContext;
        }

        public async Task CreateUserAsync(UserManager<IdentityUser> userManager, IdentityUser identityUser, string? password)
        {
            await userManager.CreateAsync(identityUser, password);
        }

        public async Task SaveUserDetailsAsync(UserDetails userDetails)
        {
            await userDbContext.UserDetails.AddAsync(userDetails);
            await userDbContext.SaveChangesAsync();
        }

        public async Task<UserDetails> GetUserDetailsAsync(string? username)
        {
            var result = await userDbContext.UserDetails.Where(u => u.Username == username).FirstOrDefaultAsync();
            
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
            var res = await userDbContext.UserDetails.Where(x => x.Username == userName).FirstOrDefaultAsync();
            if (res != null)
            {
                res.TotalCost += Amount;
            }
            await userDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<DateTimeOffset?> UserCreatedDateAsync(string? userName)
        {
            var userCreatedDate = await userDbContext.UserDetails.Where(u => u.Username == userName)
                .Select(d => d.Created).FirstOrDefaultAsync();

            return userCreatedDate;
        }

        public async Task<List<UserListData>> GetAllUserAsync()
        {
            MemoryStream ms = new MemoryStream();
            var allUsers = await userDbContext.UserDetails.Select(d => new UserListData
            {
                Id = d.Id,
                Name = d.Name,
                Username = d.Username,
                Photo = ImageForViewOrSave.ByteArrayToImageUrl(d.Photo)
            }).ToListAsync();

            return allUsers;
        }

        public async Task<UserDetailsCore?> GetUserDetailsByIdAsync(int id)
        {
            var user = await userDbContext.UserDetails.Select(d => new UserDetailsCore
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
