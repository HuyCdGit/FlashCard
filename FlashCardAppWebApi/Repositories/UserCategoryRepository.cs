using FlashCardAppWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCardAppWebApi.Repositories
{
    public class UserCategoryRepository : IUserCategoryRepository
    {
        private readonly FlashCardAppContext _dbContext;
        public UserCategoryRepository(FlashCardAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(int userId, int categoryId)
        {
            var userCategory = await _dbContext.flashcardCategories.FindAsync(userId, categoryId);

            if (userCategory == null)
            {
                throw new ArgumentException("User or category not found.");
            }

            // Create a new UserCategory entity
            var temp_userCategory = new UserCategory
            {
                UserId = userId,
                CategoryId = categoryId
            };

            // Add to the context and save changes
            _dbContext.userCategories.Add(temp_userCategory);
            await _dbContext.SaveChangesAsync();
        }

        public Task<List<UserCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserCategory?> GetUserCategorylist(int userId, int categoryId)
        {
            return await _dbContext.userCategories
                .FirstOrDefaultAsync(userCategory => userCategory.UserId == userId
                && userCategory.CategoryId == categoryId);
        }
    }
}