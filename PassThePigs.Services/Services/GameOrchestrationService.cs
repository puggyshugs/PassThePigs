using PassThePigs.Domain;
using PassThePigs.Services.Interfaces;

namespace PassThePigs.Services;

public class GameOrchestrationService : IGameOrchestrationService
{
    private readonly IGameCacheService _cacheService;
    private readonly IGameLogicService _gameLogicService;

    public GameOrchestrationService(IGameCacheService cacheService, IGameLogicService gameLogicService)
    {
        _cacheService = cacheService;
        _gameLogicService = gameLogicService;
    }

    public async Task<Guid> CreateGame()
    {
        var gameState = _cacheService.CreateGame();
        _cacheService.SaveGameState(gameState.GameId, gameState);
        return gameState.GameId;
    }

    public async Task<GameStateModel> UpdateGame(GameStateModel gameStateModel)
    {
        _cacheService.SaveGameState(gameStateModel.GameId, gameStateModel);
        var updatedState = _cacheService.GetGameState(gameStateModel.GameId);
        return updatedState;
    }

    public async Task<GameStateModel?> GetGameState(Guid gameId)
    {
        return _cacheService.GetGameState(gameId);
    }

    public async Task<GameStateModel> JoinGame(Guid gameId, string playerName)
    {
        var gameState = _cacheService.GetGameState(gameId);

        // Handle player reconnection
        var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
        if (player != null)
        {
            player.IsConnected = true;
            return gameState;
        }
        else
        {
            _gameLogicService.AddPlayer(gameId, playerName);
        }

        var updatedState = _cacheService.GetGameState(gameId);
        _cacheService.SaveGameState(gameId, updatedState);
        return updatedState;
    }

    public async Task<GameStateModel> RollPigs(GameStateModel gameStateModel)
    {
        var updatedState = _gameLogicService.PlayerRolls(gameStateModel);
        _cacheService.SaveGameState(updatedState.GameId, updatedState);
        return updatedState;
    }

    public async Task<GameStateModel> BankPoints(GameStateModel gameStateModel)
    {
        var updatedState = _gameLogicService.PlayerBanks(gameStateModel);
        _cacheService.SaveGameState(updatedState.GameId, updatedState);
        return updatedState;
    }
}
