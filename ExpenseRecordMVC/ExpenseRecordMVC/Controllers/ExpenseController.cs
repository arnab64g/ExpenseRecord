using ExpenseRecordMVC.Models;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseRecordMVC.Controllers
{
    public class ExpenseController : Controller
    {
        public readonly ICategoryService categoryService;
        private readonly SignInManager<IdentityUser> signInManager;
        public ExpenseController(ICategoryService categoryService, SignInManager<IdentityUser> signInManager) 
        {
            this.categoryService = categoryService;
            this.signInManager = signInManager;
        }
        public IActionResult ExpenseList()
        {
            return View(new List<ExpenseListView>());
        }

        public async Task<IActionResult> AddExpense()
        {
            if (signInManager.IsSignedIn(User))
            {
                var clist = await categoryService.GetUserChoicesAsync(User.Identity.Name);
                return View();
            }
            else return View(new ExpenseAddModel());
            
            
        }
    }
}
