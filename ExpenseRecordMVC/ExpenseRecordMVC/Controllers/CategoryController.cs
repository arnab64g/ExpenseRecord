using DatabaseLayer.Entities;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Edit(int id)
        {
            var res = await categoryService.UserChoiceByIdAsync(id);

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserChoice userChoice)
        {
            if(await categoryService.SaveChoiceAsync(userChoice.Id, userChoice.CategoryName) == true)
            {
                return RedirectToAction("CategoryList", "Category");
            }
            else
            {
                return View("Edit", userChoice.Id);
            }
        }

        public async Task< IActionResult> Delete(int id)
        {
            var data = await categoryService.UserChoiceByIdAsync(id);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserChoice userChoice)
        {
            if (await categoryService.DeleteChoiceAsync(userChoice.Id) == true)
            {
                return RedirectToAction("CategoryList", "Category");
            }
            else
            {
                return View("Delete", userChoice);
            }
        }
    }
}
