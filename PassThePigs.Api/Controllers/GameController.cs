using Microsoft.AspNetCore.Mvc;
using PassThePigs.Services.Interfaces;
using PassThePigsApi.Mappers;

namespace PassThePigs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameCacheService _gameCacheService;

        public GameController(IGameCacheService gameCacheService)
        {
            _gameCacheService = gameCacheService;
        }

        [HttpGet("GetGameState")]
        public async Task<IActionResult> GetGameState(Guid gameId)
        {
            var gameState = _gameCacheService.GetGameState(gameId);
            if (gameState == null)
            {
                return NotFound("No active game found.");
            }
            return Ok(GameStateMapper.ToDto(gameState));
        }

        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame()
        {
            var createdGame = _gameCacheService.CreateGame();
            return Ok(GameStateMapper.ToDto(createdGame));
        }

        [HttpDelete("EndGame")]
        // so for this, when react signals to end game, it will have to send over the game id, 
        // which will be sent to the client when the game is created
        public async Task<IActionResult> EndGame(Guid gameId)
        {
            _gameCacheService.RemoveGameState(gameId);
            return Ok(new { Message = $"Game: {gameId} ended!" });
        }
    }
}
