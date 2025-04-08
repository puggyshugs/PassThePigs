using PassThePigs.Services.Interfaces;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;
using PassThePigs.Services.Helpers.Interfaces;
using PassThePigs.Services.Helpers.Enums;

namespace PassThePigs.GameLogic.Services;

public class GameLogicService : IGameLogicService
{
    private readonly IGameCacheService _gameCacheService;
    private readonly IPlayerLogicHelper _playerLogicHelper;
    private readonly IPigThrowLogicHelper _pigThrowLogicHelper;

    public GameLogicService(IGameCacheService gameCacheService, IPlayerLogicHelper playerLogicHelper, IPigThrowLogicHelper pigThrowLogicHelper)
    {
        _gameCacheService = gameCacheService;
        _playerLogicHelper = playerLogicHelper;
        _pigThrowLogicHelper = pigThrowLogicHelper;
    }

    public GameStateModel AddPlayer(Guid gameId, string playerName)
    {
        var gameState = _gameCacheService.GetGameState(gameId);
        if (gameState == null) return new GameStateModel();

        _playerLogicHelper.AddPlayer(gameState, playerName);
        if (gameState.Message.Equals(AddPlayerMessages.PlayerAddedSuccessfully))
        {
            gameState.Message = AddPlayerMessages.PlayerAddedSuccessfully.ToString();
            return gameState;
        }
        _gameCacheService.SaveGameState(gameState.GameId, gameState);
        return gameState;
    }

    public GameStateModel RemovePlayer(Guid gameId, string playerName)
    {
        var gameState = _gameCacheService.GetGameState(gameId);
        if (gameState == null) return new GameStateModel();

        _playerLogicHelper.RemovePlayer(gameState, playerName);
        _gameCacheService.SaveGameState(gameState.GameId, gameState);
        return gameState;
    }

    public GameStateModel PlayerRolls(GameStateModel gameStateModel)
    {
        var player = gameStateModel.Players.FirstOrDefault(p => p.PlayerId == gameStateModel.CurrentPlayerId);
        if (player == null) return gameStateModel;

        bool isMakingBacon = false;
        int playerScore = player.TotalScore;
        bool turnOver = player.TurnOver;
        _pigThrowLogicHelper.HandlePigRoll(
            ref player, ref playerScore, ref turnOver, ref isMakingBacon);

        if (turnOver)
        {
            if (isMakingBacon) player.Score = 0;
            _playerLogicHelper.EndTurn(gameStateModel);
        }
        else
        {
            player.Score = playerScore;
        }

        _gameCacheService.SaveGameState(gameStateModel.GameId, gameStateModel);
        return gameStateModel;
    }

    public GameStateModel PlayerBanks(GameStateModel gameStateModel)
    {
        if (gameStateModel == null) return new GameStateModel
        {
            Message = "Game state not found"
        };

        var player = gameStateModel.Players.FirstOrDefault(p => p.PlayerId == gameStateModel.CurrentPlayerId);
        if (player == null) return new GameStateModel
        {
            Message = "Player not found"
        };

        player.TotalScore += player.Score;
        player.Score = 0;

        _playerLogicHelper.EndTurn(gameStateModel);
        _gameCacheService.SaveGameState(gameStateModel.GameId, gameStateModel);
        var updatedState = _gameCacheService.GetGameState(gameStateModel.GameId);

        return updatedState;
    }
}
