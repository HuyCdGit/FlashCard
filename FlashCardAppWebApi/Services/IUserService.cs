using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.ViewModels;

namespace FlashCardAppWebApi.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int userId); // Thêm "Async" vào tên phương thức
        Task<User?> Register(RegisterViewModel registerViewModel);
        Task<string> Login(LoginViewModel loginViewModel);
    }
}