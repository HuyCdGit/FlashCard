using FlashCardAppWebApi.Models;

namespace FlashCardAppWebApi.Services
{
    public interface ICategoryService
    {
        Task<Category?> GetByIdAsync(int categoryId); // Thêm "Async" vào tên phương thức
    }
}