using DatabaseLayer.Entities;

namespace DatabaseLayer.Interface
{
    public interface ICategoryRepository
    {
        public Task<List<UserChoice>> GetUserChoiceAsync(string? uname);
        public Task<bool> CreateCategoryAsync(UserChoice userChoice);
        public Task<UserChoice?> UserChoiceByIdAsync(int id);
        public Task<bool?> SaveChoiceAsync(int id, string? newName);
        public Task<bool?> DeleteChoiceAsync(int id);
        public Task<int> GerUserChoiceIdAsync(string? userName, string? CategoryName);
    }
}
