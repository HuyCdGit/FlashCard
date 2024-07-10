using FlashCardAppWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FlashCardAppWebApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FlashCardAppContext _dbContext;
        public CategoryRepository(FlashCardAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.categories.FindAsync(id);
        }
    }
}