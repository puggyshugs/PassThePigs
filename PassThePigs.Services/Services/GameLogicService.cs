using PassThePigs.Services.Interfaces;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;
using PassThePigs.Services.Helpers;

namespace PassThePigs.GameLogic.Services;

public class GameLogicService : IGameLogicService
{
    private readonly IGameMemoryCache _gameMemoryCache;
    private readonly PlayerLogicHelper _playerLogicHelper;

    public GameLogicService(IGameMemoryCache gameMemoryCache, PlayerLogicHelper playerLogicHelper)
    {
        _gameMemoryCache = gameMemoryCache;
        _playerLogicHelper = playerLogicHelper;
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
        int playerScore = player.Score;
        bool turnOver = player.TurnOver;
        PigThrowLogicHelper.HandlePigRoll(
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

        player.Score += player.Score;

        _playerLogicHelper.EndTurn(gameStateModel);
        _gameMemoryCache.UpdateGame(gameStateModel.GameId, gameStateModel);
        var updatedState = _gameMemoryCache.GetGameState(gameStateModel.GameId);

        return updatedState;
    }
}
