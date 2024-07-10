using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.ViewModels;

namespace FlashCardAppWebApi.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id); // Thêm "Async" vào tên phương thức
        Task<User?> GetByUserNameAsync(string userName); // Thêm "Async" vào tên phương thức
        Task<List<User>> GetAllAsync(); // Thêm "Async" vào tên phương thức
        Task<User?> AddAsync(RegisterViewModel registerViewModel); // Thêm "Async" vào tên phương thức
        Task UpdateAsync(User user); // Thêm "Async" vào tên phương thức
        Task DeleteAsync(int id); // Thêm "Async" vào tên phương thức
        Task<User?> GetByEmailAsync(string email); // Thêm "Async" vào tên phương thức
        Task<string> LoginUser(LoginViewModel loginViewModel);
    }
}