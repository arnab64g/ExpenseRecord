using DatabaseLayer.Entities;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseRecordMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ICategoryService categoryService;

        public CategoryController(SignInManager<IdentityUser> signInManager, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.categoryService = categoryService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> CategoryList()
        {
            if (signInManager.IsSignedIn(User))
            {
                string uname = User.Identity.Name;
                var list = await categoryService.GetUserChoicesAsync(uname);

                return View(list);
            }
            else
            {
                return View(new List<UserChoice>());
            }
        }
    }
}
