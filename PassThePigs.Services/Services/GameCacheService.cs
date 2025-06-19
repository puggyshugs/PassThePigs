using PassThePigs.Services.Interfaces;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;

namespace PassThePigs.Services.Services;

public class GameCacheService : IGameCacheService
{
    private readonly IGameMemoryCache _gameMemoryCache;

    public GameCacheService(IGameMemoryCache gameMemoryCache)
    {
        _gameMemoryCache = gameMemoryCache;
    }

    public GameStateModel CreateGame(string playerName)
    {
        Guid gameId = Guid.NewGuid();
        Guid playerId = Guid.NewGuid();

        string joiningCode = gameId.ToString("N")[^10..]; // "N" format removes dashes, a format specifier. [^4..] takes the last 4 characters, part of .NET 8's index range expression.

        var gameState = new GameStateModel
        {
            GameId = gameId,
            Players = new List<PlayerModel>
            {
                new PlayerModel
                {
                    PlayerId = playerId,
                    PlayerName = playerName,
                    Score = 0
                }
            },
            CurrentTurnIndex = 0,
            CurrentPlayerId = playerId,
            JoiningCode = joiningCode,
            Message = $"Game:{gameId} successfully created."
        };

        _gameMemoryCache.CreateGame(gameId, gameState);
        return gameState;
    }

    public GameStateModel SaveGameState(Guid gameId, GameStateModel gameState)
    {
        _gameMemoryCache.UpdateGame(gameId, gameState);
        gameState.Message = $"Game:{gameId} successfully saved.";
        return gameState;
    }

    public GameStateModel GetGameState(Guid gameId)
    {
        var gameState = _gameMemoryCache.GetGameState(gameId);
        if (gameState == null)
        {
            var name = "Default Player";
            return CreateGame(name);
        }
        gameState.Message = $"Game:{gameId} successfully retrieved.";
        return gameState;
    }

    public GameStateModel RemoveGameState(Guid gameId)
    {
        _gameMemoryCache.EndGame(gameId);
        return new GameStateModel { GameId = Guid.Empty, Message = $"Game:{gameId} successfully removed." };
    }
}