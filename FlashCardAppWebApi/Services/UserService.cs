using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.Repositories;
using FlashCardAppWebApi.ViewModels;

namespace FlashCardAppWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetByIdAsync(int userId)
        { 
            User? user = await _userRepository.GetByIdAsync(userId);
            return user;
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            return await _userRepository.LoginUser(loginViewModel);
        }

        public async Task<User?> Register(RegisterViewModel registerViewModel)
        {
            // Kiểm tra xem username hoặc email đã tồn tại chưa
            //tạo đối tượng user dựa trên registerviewmodel

            var existingUser = await _userRepository.GetByUserNameAsync(registerViewModel.Username ?? "");
            if (existingUser != null)
            {
                // Username đã tồn tại
                throw new ArgumentException("Username already exisist");
            }

            existingUser = await _userRepository.GetByEmailAsync(registerViewModel.Email);
            if (existingUser != null)
            {
                // Email đã tồn tại
                throw new ArgumentException("Email already exisist");
            }

            return await _userRepository.AddAsync(registerViewModel);
        }
    }
}

