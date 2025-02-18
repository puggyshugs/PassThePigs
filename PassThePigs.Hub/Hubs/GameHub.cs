using Microsoft.AspNetCore.SignalR;
using PassThePigs.Domain;
using PassThePigs.Services.Interfaces;

namespace PassThePigs.Hub.Hubs;
public class GameHub : Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IGameCacheService _cacheService;

    public GameHub(IGameCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task CreateGame()
    {
        var gameId = Guid.NewGuid();
        var gameState = _cacheService.CreateGame();

        _cacheService.SaveGameState(gameId, gameState);
        await Clients.Caller.SendAsync("GameCreated", gameId);
    }

    public async Task SaveGame(Guid gameId, GameStateModel gameState)
    {
        _cacheService.SaveGameState(gameId, gameState);
        await Clients.Caller.SendAsync("GameSaved", gameId);
    }

    public async Task RollPigs(Guid gameId, string playerId)
    {
        var gameState = _cacheService.GetGameState(gameId);
        if (gameState == null) return;

        // Apply game logic to process the roll
        //var updatedState = _gameLogic.ProcessRoll(gameState, playerId);

        // Save updated game state
        //_cacheService.SaveGameState(gameId, updatedState);

        // Send updated game state to all players
        //await Clients.Group(gameId.ToString()).SendAsync("GameUpdated", updatedState);
    }

    public async Task GetGameState(Guid gameId)
    {
        var gameState = _cacheService.GetGameState(gameId);
        await Clients.Caller.SendAsync("GameStateRetrieved", gameState);
    }

    public async Task JoinGame(string gameId, string playerName)
    {
        // _gameCacheService.AddPlayer(gameId, playerName);
        await Clients.All.SendAsync("PlayerJoined", playerName);
    }
}

