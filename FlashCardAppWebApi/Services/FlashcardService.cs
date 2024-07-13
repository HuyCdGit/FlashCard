using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.Repositories;

namespace FlashCardAppWebApi.Services
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardReposotory _flashcardReposotory;

        public FlashcardService(IFlashcardReposotory flashcardReposotory)
        {
            _flashcardReposotory = flashcardReposotory;
        }

        public async Task<List<Flashcard>?> GetFlashcards(int page, int limit)
        {
            return await _flashcardReposotory.GetFlashcardsRealtime(page, limit);
        }
    }
}