using FlashCardAppWebApi.Models;

namespace FlashCardAppWebApi.Repositories
{
    public interface IFlashcardReposotory
    {
        Task<List<Flashcard>?> GetFlashcardsRealtime(int page, int limit);
    }
}