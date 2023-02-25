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
        private readonly IUserService userService;

        public FilterController(SignInManager<IdentityUser> signInManager, ICategoryService categoryService, 
            IExpenseService expenseService, IUserService userService)
        {
            this.signInManager = signInManager;
            this.categoryService = categoryService;
            this.expenseService = expenseService;
            this.userService = userService;
        }

        public async Task<IActionResult> FilterPartial()
        {
            FilterModel filterModel = new FilterModel();
            
            if (signInManager.IsSignedIn(User))
            {
                filterModel.FromDate = await userService.UserCreatedDateAsync(User.Identity.Name);
                filterModel.ToDate = DateTime.Now;
                
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

            var data = new FilteredViewModel();
            
            if (signInManager.IsSignedIn(User))
            {
                var list = await expenseService.GetExpenseByDate(filterModel.FromDate, filterModel.ToDate, User.Identity.Name);
                
                if (filterModel.CategoryFilters.Count != 0 && data != null)
                {
                    list = expenseService.FilterByCategoryAsync(list, filterModel.CategoryFilters);
                }

                if (list == null)
                {
                    list = new List<ExpenseView>();
                }

                data.expenseViews = list;
                data.FromDate = filterModel.FromDate;
                data.ToDate = filterModel.ToDate;
                data.TotalCost = 0;

                foreach(var item in list)
                {
                    data.TotalCost += item.Amount;
                }
            }

            return View("FilteredList", data);
        }
    }
}
