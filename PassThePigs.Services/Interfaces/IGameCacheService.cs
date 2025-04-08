using PassThePigs.Domain;

namespace PassThePigs.Services.Interfaces;

public interface IGameCacheService
{
    GameStateModel CreateGame();
    GameStateModel SaveGameState(Guid gameId, GameStateModel gameState);
    GameStateModel GetGameState(Guid gameId);
    GameStateModel RemoveGameState(Guid gameId);
}
