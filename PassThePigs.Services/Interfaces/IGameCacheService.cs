using System;
using PassThePigs.Services.Cache.CacheModels;

namespace PassThePigs.Services.Interfaces;

public interface IGameCacheService
{
    GameStateModel CreateGame();
    GameStateModel SaveGameState(Guid gameId, GameStateModel gameState);
    GameStateModel? GetGameState(Guid gameId);
    GameStateModel RemoveGameState(Guid gameId);
}
