using System;
using PassThePigs.Domain;

namespace PassThePigs.Services.Interfaces;

public interface IGameLogicService
{
    bool AddPlayer(Guid gameId, string playerName);
    bool RemovePlayer(GameStateModel gameStateModel);
    GameStateModel PlayerRolls(GameStateModel gameStateModel);
    GameStateModel PlayerBanks(GameStateModel gameStateModel);
}
