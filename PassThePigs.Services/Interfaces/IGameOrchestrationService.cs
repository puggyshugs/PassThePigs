using PassThePigs.Domain;

namespace PassThePigs.Services.Interfaces;

public interface IGameOrchestrationService
{
    Task<Guid> CreateGame();
    Task<GameStateModel> UpdateGame(GameStateModel gameStateModel);
    Task<GameStateModel?> GetGameState(Guid gameId);
    Task<GameStateModel> JoinGame(Guid gameId, string playerName);
    Task<GameStateModel> RollPigs(GameStateModel gameStateModel);
    Task<GameStateModel> BankPoints(GameStateModel gameStateModel);
}
