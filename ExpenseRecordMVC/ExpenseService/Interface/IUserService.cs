using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpenseService.Interface
{
    public interface IUserService
    {
        public Task<UserDetails> CreateNewUserAsync(UserManager<IdentityUser> userManager, UserDetails userDetails, string pssword);
        public Task<UserDetails> GetUserDetailsAsync(string username);
        public Task<DateTimeOffset?> UserCreatedDateAsync(string? userName);
        public Task<List<UserListData>> GetAllUsersAsync();
        public Task<UserDetailsCore?> GetUserDetailsByIdAsync(int id);
    }
}
