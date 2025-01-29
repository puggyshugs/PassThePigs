using Microsoft.AspNetCore.Mvc;
using PassThePigs.Services.Cache.Interfaces;
using PassThePigs.Services.Interfaces;

namespace PassThePigs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameMemoryCache _gameCache;
        private IGameService _gameService;

        public GameController(IGameService gameService, IGameMemoryCache gameCache)
        {
            _gameService = gameService;
            _gameCache = gameCache;
        }

        [HttpGet("state")]
        public IActionResult GetGameState()
        {
            return Ok("Not implemented yet.");
            // var gameState = _gameCache.GetGameState();
            // if (gameState == null)
            // {
            //     return NotFound("No active game found.");
            // }
            // return Ok(gameState);
        }
    }
}
