using PassThePigs.Services.Interfaces;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;
using PassThePigs.Services.Helpers;
using PassThePigs.Services.Helpers.Interfaces;

namespace PassThePigs.GameLogic.Services;

public class GameLogicService : IGameLogicService
{
    private readonly IGameMemoryCache _gameMemoryCache;
    private readonly IPlayerLogicHelper _playerLogicHelper;
    private readonly IPigThrowLogicHelper _pigThrowLogicHelper;

    public GameLogicService(IGameMemoryCache gameMemoryCache, IPlayerLogicHelper playerLogicHelper, IPigThrowLogicHelper pigThrowLogicHelper)
    {
        _gameMemoryCache = gameMemoryCache;
        _playerLogicHelper = playerLogicHelper;
        _pigThrowLogicHelper = pigThrowLogicHelper;
    }

    public bool AddPlayer(Guid gameId, string playerName)
    {
        var gameState = _gameMemoryCache.GetGameState(gameId);
        if (gameState == null) return false;

        bool playerAdded = _playerLogicHelper.AddPlayer(ref gameState, playerName);
        if (playerAdded)
        {
            _gameMemoryCache.UpdateGame(gameState.GameId, gameState);
        }
        return playerAdded;
    }

    public bool RemovePlayer(GameStateModel gameStateModel)
    {
        var result = _playerLogicHelper.RemovePlayer(gameStateModel);
        _gameMemoryCache.UpdateGame(gameStateModel.GameId, gameStateModel);
        return result;
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

        _gameMemoryCache.UpdateGame(gameStateModel.GameId, gameStateModel);
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
        _gameMemoryCache.UpdateGame(gameStateModel.GameId, gameStateModel);
        var updatedState = _gameMemoryCache.GetGameState(gameStateModel.GameId);

        return updatedState;
    }
}
