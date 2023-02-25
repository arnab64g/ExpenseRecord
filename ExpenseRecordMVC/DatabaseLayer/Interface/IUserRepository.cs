using DatabaseLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace DatabaseLayer.Interface
{
    public interface IUserRepository
    {
        public Task CreateUserAsync(UserManager<IdentityUser> userManager, IdentityUser identityUser, string? password);
        public Task SaveUserDetailsAsync(UserDetails userDetails);
        public Task<UserDetails> GetUserDetailsAsync(string? username);
        public Task<bool> ChangeTotalAmountAsync(string? userName, decimal? Amount);
        public Task<DateTimeOffset?> UserCreatedDateAsync(string? userName);
    }
}
