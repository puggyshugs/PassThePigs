using System;
using PassThePigs.Domain;

namespace PassThePigs.Services.Helpers.Interfaces;

public interface IPlayerLogicHelper
{
    bool AddPlayer(ref GameStateModel gameState, string playerName);
    bool RemovePlayer(GameStateModel gameState);
    void EndTurn(GameStateModel gameState);
    void HandlePlayerReconnection(GameStateModel gameState, string playerName);
    public void HandlePlayerDisconnection(GameStateModel gameState, string playerName);
}
