using System.Collections.Concurrent;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;

namespace PassThePigs.Data.Cache;

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
        /*  _cachedGamesList.AddOrUpdate(gameId, updatedState, (key, existingState) => updatedState); 
            Above is an alternative to TryUpdate, but TryUpdate is better for turn based games as it prevents overwriting.
            If i change the game so multiple updates via multiple users are possible TryUpdate could start causing 
            race conditions, should switch to AddOrUpdate.
        
            TryUpdate is built into concurrent dictionary and only updates the game state
            if the game state hasn't been updated since last read, preventing simultaneous updates */

        bool updated = _cachedGamesList.TryUpdate(gameId, updatedState, _cachedGamesList[gameId]);
        if (!updated)
        {
            throw new InvalidOperationException($"Game state update failed for Game ID: {gameId}. Possible concurrent modification.");
        }
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