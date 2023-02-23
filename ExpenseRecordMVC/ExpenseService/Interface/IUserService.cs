using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpenseService.Interface
{
    public interface IUserService
    {
        public Task<UserDetails> CreateNewUserAsync(UserManager<IdentityUser> userManager, UserDetails userDetails, string pssword);
        public Task<UserDetails> GetUserDetailsAsync(string username);
    }
}
