using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PassThePigs.Services.Interfaces;

namespace PassThePigs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameCacheService _gameCacheService;

        public GameController(IGameCacheService gameCacheService)
        {
            _gameCacheService = gameCacheService;
        }

        [HttpGet("GetGameState")]
        public IActionResult GetGameState(Guid gameId)
        {
            var gameState = _gameCacheService.GetGameState(gameId);
            if (gameState == null)
            {
                return NotFound("No active game found.");
            }
            return Ok(gameState);
        }

        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame()
        {
            var createdGame = _gameCacheService.CreateGame();
            var x = AddPlayer(createdGame.GameId, "Player1");
            return Ok(createdGame);
        }

        [HttpDelete("EndGame")]
        // so for this, when react signals to end game, it will have to send over the game id, 
        // which will be sent to the client when the game is created
        public IActionResult EndGame(Guid gameId)
        {
            _gameCacheService.RemoveGameState(gameId);
            return Ok(new { Message = $"Game: {gameId} ended!" });
        }

        private string AddPlayer(Guid gameId, string playerName)
        {
            // _gameCacheService.AddPlayer(gameId, playerName);
            return "Player added!";
        }
    }
}
