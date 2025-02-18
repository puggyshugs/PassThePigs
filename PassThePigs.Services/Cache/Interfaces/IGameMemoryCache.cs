using System;
using PassThePigs.Domain;
using PassThePigs.Services.Cache.CacheModels;

namespace PassThePigs.Services.Cache.Interfaces;

public interface IGameMemoryCache
{
    GameStateModel CreateGame(Guid gameId, GameStateModel gameState);
    public void UpdateGame(Guid gameId, GameStateModel gameState);
    GameStateModel? GetGameState(Guid gameId);
    void EndGame(Guid gameId);
}
