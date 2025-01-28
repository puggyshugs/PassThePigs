using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassThePigs.Services.Interfaces;

namespace PassThePigs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
    }
}
