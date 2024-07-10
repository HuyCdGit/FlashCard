using FlashCardAppWebApi.Models;

namespace FlashCardAppWebApi.Repositories
{
    public interface IUserCategoryRepository
    {
        Task<UserCategory?> GetUserCategorylist(int userId, int categoryId); // Thêm "Async" vào tên phương thức
        Task<List<UserCategory>> GetAllAsync(); // Thêm "Async" vào tên phương thức
        Task AddAsync(int userId, int categoryId); // Thêm "Async" vào tên phương thức
    }
}