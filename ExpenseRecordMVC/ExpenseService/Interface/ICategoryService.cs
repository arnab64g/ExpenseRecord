using DatabaseLayer.Entities;

namespace ExpenseService.Interface
{
    public interface ICategoryService
    {
        public Task<List<UserChoice>> GetUserChoicesAsync(string? uname);
        public Task<bool> CreateCategoryAsync(UserChoice userChoice);
        public Task<UserChoice?> UserChoiceByIdAsync(int id);
        public Task<bool?> SaveChoiceAsync(int id, string? newName);
        public Task<bool?> DeleteChoiceAsync(int id);
        public Task<int> GerUserChoiceIdAsync(string? userName, string? CategoryName);
    }
}
