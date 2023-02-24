using DatabaseLayer.Entities;
using ExpenseRecordMVC.Models;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseRecordMVC.Controllers
{
    public class ExpenseController : Controller
    {
        public readonly ICategoryService categoryService;
        private readonly IExpenseService expenseService;
        private readonly SignInManager<IdentityUser> signInManager;

        public ExpenseController(ICategoryService categoryService, SignInManager<IdentityUser> signInManager, IExpenseService expenseService) 
        {
            this.categoryService = categoryService;
            this.signInManager = signInManager;
            this.expenseService = expenseService;
        }

        public async Task<IActionResult> ExpenseList()
        {
            if (signInManager.IsSignedIn(User))
            {
                var list = await expenseService.GetExpenseListAsync(User.Identity.Name);

                return View(list);
            }
            return View(new List<ExpenseView>());
        }

        public async Task<IActionResult> AddExpense()
        {
            ExpenseAddModel expenseAddModel = new ExpenseAddModel();

            if (signInManager.IsSignedIn(User))
            {
                var clist = await categoryService.GetUserChoicesAsync(User.Identity.Name);

                if (clist == null)
                {
                    clist = new List<UserChoice>();
                }

                expenseAddModel.Categories = clist.Select(d => d.CategoryName).ToList();
            }

            return View(expenseAddModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(ExpenseAddModel expenseAddModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                int catId = await categoryService.GerUserChoiceIdAsync(User.Identity.Name, expenseAddModel.CategoryName);
                
                var expense = new Expense
                {
                    Date = expenseAddModel.Date,
                    CategoryId= catId,
                    Amount= expenseAddModel.Amount,
                    Username = User.Identity.Name,
                };

                var res = await expenseService.AddExpenseAsync(expense);

                if (res)
                {
                    return RedirectToAction("ExpenseList", "Expense");
                }
            }

            return View(new ExpenseAddModel());
        }

        public async Task<IActionResult> Delete(int id)
        {
            var expense = await expenseService.GetExpenseViewByIdAsync(id);

            return View("DeleteExpense",expense);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ExpenseView expenseView)
        {
            await expenseService.DeleteExpenseById(expenseView.Id);

            return RedirectToAction("ExpenseList", "Expense");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ExpenseEditModel expenseEditModel = new ExpenseEditModel();
            if (signInManager.IsSignedIn(User))
            {
                var expense = await expenseService.GetExpenseViewByIdAsync(id);
                var cat = await categoryService.GetUserChoicesAsync(User.Identity.Name);

                expenseEditModel.Id = expense.Id;
                expenseEditModel.CategoryName = expense.CategoryName;
                expenseEditModel.Amount = expense.Amount;
                expenseEditModel.Categories = cat.Select(d => d.CategoryName).ToList();
                expenseEditModel.Date = expense.Date;
            }

            return View(expenseEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseEditModel expenseEditModel)
        {
            if (signInManager.IsSignedIn(User))
            {
                Expense exp = new Expense
                {
                    Id = expenseEditModel.Id,
                    Date = expenseEditModel.Date,
                    Username = User.Identity.Name,
                    Amount = expenseEditModel.Amount,
                    CategoryId = await categoryService.GerUserChoiceIdAsync(User.Identity.Name, expenseEditModel.CategoryName)
                };

                await expenseService.UpdateExpenseAsync(exp);

                return RedirectToAction("ExpenseList", "Expense");
            }

            return RedirectToAction("Edit", "Expense");
        }
    }
}
