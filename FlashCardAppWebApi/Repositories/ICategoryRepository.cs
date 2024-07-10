using FlashCardAppWebApi.Models;

namespace FlashCardAppWebApi.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id); // Thêm "Async" vào tên phương thức
    }
}