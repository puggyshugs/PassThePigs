using System;
using PassThePigs.Domain;

namespace PassThePigs.Services.Helpers.Interfaces;

public interface IPlayerLogicHelper
{
    void AddPlayer(GameStateModel gameState, string playerName);
    void RemovePlayer(GameStateModel gameState, string playerName);
    void EndTurn(GameStateModel gameState);
    void HandlePlayerReconnection(GameStateModel gameState, string playerName);
    public void HandlePlayerDisconnection(GameStateModel gameState, string playerName);
}
