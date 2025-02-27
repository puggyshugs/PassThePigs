using System.Collections.Concurrent;
using PassThePigs.Domain;
using PassThePigs.Services.Cache.CacheModels;
using PassThePigs.Services.Cache.Interfaces;

namespace PassThePigs.Services.Services;

public class GameMemoryCache : IGameMemoryCache
{
    private readonly ConcurrentDictionary<Guid, GameStateModel> _cachedGamesList = new();

    public GameStateModel CreateGame(Guid gameId, GameStateModel gameState)
    {
        _cachedGamesList[gameId] = gameState;
        return gameState;
    }

    public void UpdateGame(Guid gameId, GameStateModel updatedState)
    {
        // TryUpdate is built into concurrent dictionary and only updates the game state
        // if the game state hasn't been updated since last read, preventing simultaneous updates
        _cachedGamesList.TryUpdate(gameId, updatedState, _cachedGamesList[gameId]);
    }

    public GameStateModel? GetGameState(Guid gameId)
    {
        return _cachedGamesList.TryGetValue(gameId, out GameStateModel? gameState) ? gameState : null;
    }

    public void EndGame(Guid gameId)
    {
        _cachedGamesList.TryRemove(gameId, out _);
    }
}