using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.Repositories;

namespace FlashCardAppWebApi.Services
{
    public class UserCategoryService : IUserCategoryService
    {
        private readonly IUserCategoryRepository _userCategoryRepository;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository)
        {
            _userCategoryRepository = userCategoryRepository;
        }

        public async Task AddAsync(int userId, int categoryId)
        {
            await _userCategoryRepository.AddAsync(userId, categoryId);
        }

        public async Task<UserCategory?> GetUserCategorylist(int userId, int categoryId)
        {
            UserCategory? userCategory = await _userCategoryRepository.GetUserCategorylist(userId, categoryId);
            return userCategory;
        }
    }
}