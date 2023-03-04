using DatabaseLayer.Entities;
using ExpenseRecordMVC.Models;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ExtraLibraries;

namespace ExpenseRecordMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IUserService userService;

        public UserController(UserManager<IdentityUser> userManager, IUserService userService, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var res = await signInManager.PasswordSignInAsync(NotNull.MakeNotNull(loginModel.UsernameOrPassword), NotNull.MakeNotNull(loginModel.Password), true, true);
            
            if (res.Succeeded)
            {
                return RedirectToAction("ExpenseList", "Expense");
            }

            return View(loginModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var ms = new MemoryStream();

            if (signUpModel.ImageUrl != null)
            {
                signUpModel.ImageUrl.CopyTo(ms);
            }

            var userDetaild = new UserDetails
            {
                Name = signUpModel.Name,
                Email = signUpModel.Email,
                Username = signUpModel.Username,
                Phones = signUpModel.Phone,
                Address = signUpModel.Address,
                TotalCost = 0,
                Created = DateTimeOffset.Now,
                Photo = ms.ToArray(),
            };

            await userService.CreateNewUserAsync(userManager, userDetaild, NotNull.MakeNotNull(signUpModel.Password));

            return View("Login");
        }

        public async Task<IActionResult> Profile()
        {
            if (signInManager.IsSignedIn(User))
            {
                if (User.Identity == null)
                {
                    return BadRequest(ModelState);
                }

                var result = await userService.GetUserDetailsAsync(NotNull.MakeNotNull(User.Identity.Name));

                return View(result);
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Login", "User");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await userService.GetAllUsersAsync();

            return View("AllUserData", data);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var data = await userService.GetUserDetailsByIdAsync(id);

            return View(data);
        }
    }
}
