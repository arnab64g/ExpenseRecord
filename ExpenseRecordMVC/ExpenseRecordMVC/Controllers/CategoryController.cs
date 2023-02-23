using DatabaseLayer.Entities;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseRecordMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ICategoryService categoryService;

        public CategoryController(SignInManager<IdentityUser> signInManager, ICategoryService categoryService)
        {
            this.signInManager = signInManager;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryList()
        {
            if (signInManager.IsSignedIn(User))
            {
                var list = await categoryService.GetUserChoicesAsync(User.Identity.Name);

                return View(list);
            }
            else
            {
                return View(new List<UserChoice>());
            }
        }

        public IActionResult AddNewCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(UserChoice userChoice)
        {
            if (signInManager.IsSignedIn(User))
            {
                userChoice.Username = User.Identity.Name;
                if (await categoryService.CreateCategoryAsync(userChoice))
                {
                    return RedirectToAction("CategoryList", "Category");
                }
            }

            return View();
        }
    }
}
