using System;
using PassThePigs.Services.Cache.CacheModels;

namespace PassThePigs.Services.Cache.Interfaces;

public interface IGameMemoryCache
{
    void AddPlayer(string gameId, string playerName);
    void UpdateScore(string gameId, string playerName, int score);
    GameStateModel GetGameState(string gameId);
    void EndGame(string gameId);
}
