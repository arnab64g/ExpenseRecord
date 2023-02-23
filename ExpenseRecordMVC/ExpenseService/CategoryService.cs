using DatabaseLayer.Entities;
using DatabaseLayer.Interface;
using ExpenseService.Interface;

namespace ExpenseService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<List<UserChoice>> GetUserChoicesAsync(string uname)
        {
            return await categoryRepository.GetUserChoiceAsync(uname);
        }

        public async Task<bool> CreateCategoryAsync(UserChoice userChoice)
        {
            return await categoryRepository.CreateCategoryAsync(userChoice);
        }
    }
}
