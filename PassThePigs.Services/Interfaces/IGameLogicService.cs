using PassThePigs.Domain;

namespace PassThePigs.Services.Interfaces;

public interface IGameLogicService
{
    GameStateModel AddPlayer(Guid gameId, string playerName);
    GameStateModel RemovePlayer(Guid gameId, string playerName);
    GameStateModel PlayerRolls(GameStateModel gameStateModel);
    GameStateModel PlayerBanks(GameStateModel gameStateModel);
}
