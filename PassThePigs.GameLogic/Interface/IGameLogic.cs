using System;
using PassThePigs.Services.Cache.CacheModels;

namespace PassThePigs.GameLogic.Interface;

public interface IGameLogic
{
    GameStateModel ProcessRoll(GameStateModel gameState, string playerId);
}
