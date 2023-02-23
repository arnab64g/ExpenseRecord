using DatabaseLayer.Entities;

namespace ExpenseService.Interface
{
    public interface ICategoryService
    {
        public Task<List<UserChoice>> GetUserChoicesAsync(string uname);
        public Task<bool> CreateCategoryAsync(UserChoice userChoice);
    }
}
