using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using ExpenseService.Interface;

namespace ExpenseService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IExpenseRepository expenseRepository;

        public CategoryService(ICategoryRepository categoryRepository, IExpenseRepository expenseRepository)
        {
            this.categoryRepository = categoryRepository;
            this.expenseRepository = expenseRepository;
        }

        public async Task<List<UserChoice>> GetUserChoicesAsync(string? uname)
        {
            var list = await categoryRepository.GetUserChoiceAsync(uname);
            return list.OrderBy(d => d.CategoryName).ToList();
        }

        public async Task<bool> CreateCategoryAsync(UserChoice userChoice)
        {
            return await categoryRepository.CreateCategoryAsync(userChoice);
        }

        public async Task<UserChoice?> UserChoiceByIdAsync(int id)
        {
            return await categoryRepository.UserChoiceByIdAsync(id);
        }

        public async Task<bool?> SaveChoiceAsync(int id, string? newName)
        {
            return await categoryRepository.SaveChoiceAsync(id, newName);
        }

        public async Task<bool?> DeleteChoiceAsync(int id)
        {
            if (await expenseRepository.IsUsedCategory(id))
            {
                return false;
            }
            else
            {
                return await categoryRepository.DeleteChoiceAsync(id);
            }
        }

        public async Task<int> GerUserChoiceIdAsync(string? userName, string? CategoryName)
        {
            return await categoryRepository.GerUserChoiceIdAsync(userName, CategoryName);
        }
    }
}
