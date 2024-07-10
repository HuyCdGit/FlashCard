using FlashCardAppWebApi.Models;

namespace FlashCardAppWebApi.Services
{
    public interface IUserCategoryService
    {
        Task<UserCategory?> GetUserCategorylist(int userId, int categoryId);
        Task AddAsync(int userId, int categoryId); // Thêm "Async" vào tên phương thức
    }
}