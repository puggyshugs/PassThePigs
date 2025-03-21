using PassThePigs.Services.Interfaces;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;

namespace PassThePigs.Services.Services;

public class GameCacheService : IGameCacheService
{
    private readonly IGameMemoryCache _gameMemoryCache;

    public GameCacheService(IGameMemoryCache gameMemoryCache)
    {
        _gameMemoryCache = gameMemoryCache;
    }

    public GameStateModel CreateGame()
    {
        Guid gameId = Guid.NewGuid();
        var gameState = new GameStateModel { GameId = gameId, Message = $"Game:{gameId} successfully created." };
        _gameMemoryCache.CreateGame(gameId, gameState);
        return gameState;
    }

    public GameStateModel SaveGameState(Guid gameId, GameStateModel gameState)
    {
        _gameMemoryCache.UpdateGame(gameId, gameState);
        gameState.Message = $"Game:{gameId} successfully saved.";
        return gameState;
    }

    public GameStateModel GetGameState(Guid gameId)
    {
        var gameState = _gameMemoryCache.GetGameState(gameId);
        if (gameState == null)
        {
            throw new KeyNotFoundException($"Game {gameId} not found.");
        }
        gameState.Message = $"Game:{gameId} successfully retrieved.";
        return gameState;
    }

    public GameStateModel RemoveGameState(Guid gameId)
    {
        _gameMemoryCache.EndGame(gameId);
        return new GameStateModel { GameId = Guid.Empty, Message = $"Game:{gameId} successfully removed." };
    }
}