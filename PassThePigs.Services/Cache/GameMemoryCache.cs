using System.Collections.Concurrent;
using PassThePigs.Services.Cache.CacheModels;
using PassThePigs.Services.Cache.Interfaces;

namespace PassThePigs.Services.Cache;

public class GameMemoryCache : IGameMemoryCache
{
    private readonly ConcurrentDictionary<string, GameStateModel> _games = new();

    public void AddPlayer(string gameId, string playerName)
    {
        if (!_games.ContainsKey(gameId))
        {
            _games[gameId] = new GameStateModel();
        }
        _games[gameId].Players.Add(new PlayerModel { PlayerName = playerName, Score = 0 });
    }

    public void UpdateScore(string gameId, string playerName, int score)
    {
        if (_games.TryGetValue(gameId, out var gameState))
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
            if (player != null)
            {
                player.Score += score;
            }
        }
    }

    public GameStateModel GetGameState(string gameId)
    {
        try
        {
            var currentGameState = _games.TryGetValue(gameId, out var gameState) ? gameState : null;
            return currentGameState ?? new GameStateModel();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void EndGame(string gameId)
    {
        _games.TryRemove(gameId, out _);
    }

}
