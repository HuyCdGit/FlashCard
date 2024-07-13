using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace FlashCardAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlashcardController : ControllerBase
    {
        private readonly IFlashcardService _flashcardService;
        public FlashcardController(IFlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        [HttpGet("ws")]
        public async Task GetRealtimeFlashcard(int page = 1, int limit = 10)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSockets = await HttpContext.WebSockets.AcceptWebSocketAsync();

                while (webSockets.State == WebSocketState.Open)
                {
                    List<Flashcard>? flashcards = await _flashcardService.GetFlashcards(page, limit);
                    string jsonString = JsonSerializer.Serialize(flashcards);
                    var buffer = Encoding.UTF8.GetBytes(jsonString);
                    await webSockets.SendAsync(new ArraySegment<byte>(buffer)
                        , WebSocketMessageType.Text
                        , true
                        , CancellationToken.None);
                    await Task.Delay(2000);
                }
                await webSockets.CloseAsync(WebSocketCloseStatus.NormalClosure
                    , "Connection closed by the sever"
                    , CancellationToken.None);
            }
        }
    }
}