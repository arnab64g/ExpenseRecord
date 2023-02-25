using DatabaseLayer.Entities;
using ExpenseRecordMVC.Models;
using ExpenseService.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseRecordMVC.Controllers
{
    public class FilterController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ICategoryService categoryService;
        private readonly IExpenseService expenseService;

        public FilterController(SignInManager<IdentityUser> signInManager, ICategoryService categoryService, IExpenseService expenseService)
        {
            this.signInManager = signInManager;
            this.categoryService = categoryService;
            this.expenseService = expenseService;
        }

        public async Task<IActionResult> FilterPartial()
        {
            FilterModel filterModel = new FilterModel();
            

            filterModel.FromDate = DateTime.Now;
            filterModel.ToDate = DateTime.Now;
            
            if (signInManager.IsSignedIn(User))
            {
                var flist = await categoryService.GetUserChoicesAsync(User.Identity.Name);
                if (flist != null)
                {
                    filterModel.CategoryFilters = flist.Select(d => d.CategoryName).ToList();
                }
                else
                {
                    filterModel.CategoryFilters = new List<string>();
                }
                
            }

            return View(filterModel);
        }

        [HttpPost]
        public async Task<IActionResult> FilterPartial(FilterModel filterModel) 
        {
            if (filterModel.CategoryFilters == null)
            {
                filterModel.CategoryFilters = new List<string>();
            }

            var data = new List<ExpenseView>();
            
            if (signInManager.IsSignedIn(User))
            {
                data = await expenseService.GetExpenseByDate(filterModel.FromDate, filterModel.ToDate, User.Identity.Name);
                
                if (filterModel.CategoryFilters.Count != 0 && data != null)
                {
                    data = expenseService.FilterByCategoryAsync(data, filterModel.CategoryFilters);
                }
            }

            return View("FilteredList", data);
        }
    }
}
