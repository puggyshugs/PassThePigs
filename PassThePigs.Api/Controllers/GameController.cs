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
        private readonly IGameLogicService _gameLogicService;


        public GameController(IGameCacheService gameCacheService, IGameLogicService gameLogicService)
        {
            _gameCacheService = gameCacheService;
            _gameLogicService = gameLogicService;
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

        [HttpPost("RemovePlayer")]
        public async Task<IActionResult> RemovePlayer(Guid gameId, string playerName)
        {
            var updatedGameState = _gameLogicService.RemovePlayer(gameId, playerName);
            if (updatedGameState == null)
            {
                return NotFound("No active game found.");
            }
            return Ok(GameStateMapper.ToDto(updatedGameState));
        }

        [HttpPost("AddPlayer")]
        public async Task<IActionResult> AddPlayer(Guid gameId, string playerName)
        {
            var updatedGameState = _gameLogicService.AddPlayer(gameId, playerName);
            if (updatedGameState == null)
            {
                return NotFound("No active game found.");
            }
            return Ok(GameStateMapper.ToDto(updatedGameState));
        }

        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame(string playerName)
        {
            var createdGame = _gameCacheService.CreateGame(playerName);
            return Ok(GameStateMapper.ToDto(createdGame));
        }

        [HttpDelete("EndGame")]
        public async Task<IActionResult> EndGame(Guid gameId)
        {
            _gameCacheService.RemoveGameState(gameId);
            return Ok(new { Message = $"Game: {gameId} ended!" });
        }
    }
}
