using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;

namespace ExpenseService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserDetails> CreateNewUserAsync(UserManager<IdentityUser> userManager, UserDetails userDetails, string pssword)
        {
            var user = new IdentityUser
            {
                UserName = userDetails.Username,
                Email = userDetails.Email,
            };
            await userRepository.CreateUserAsync(userManager, user, pssword);
            await userRepository.SaveUserDetailsAsync(userDetails);

            return userDetails;
        }

        public async Task<UserDetails> GetUserDetailsAsync(string username)
        {
            return await userRepository.GetUserDetailsAsync(username);
        }
    }
}