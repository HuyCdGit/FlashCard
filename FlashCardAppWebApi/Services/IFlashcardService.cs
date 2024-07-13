using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCardAppWebApi.Models;

namespace FlashCardAppWebApi.Services
{
    public interface IFlashcardService
    {
        Task<List<Flashcard>?> GetFlashcards(int page, int limit);
    }
}