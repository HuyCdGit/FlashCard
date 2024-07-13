using FlashCardAppWebApi.Models;

namespace FlashCardAppWebApi.Repositories
{
    public class FlashcardRepository : IFlashcardReposotory
    {
        private readonly FlashCardAppContext _dbContext;

        public FlashcardRepository(FlashCardAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Flashcard>?> GetFlashcardsRealtime(int page, int limit)
        {
            var query = _dbContext.flashcards
                // .Where(f => f.Image != null)
                .OrderBy(f => f.Id) // Sắp xếp theo Id hoặc một trường khác
                .Skip((page - 1) * limit)
                .Take(limit);

            var flashcards = query.ToList();
            return flashcards;
        }
    }
}