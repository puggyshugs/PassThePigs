using Microsoft.AspNetCore.SignalR;
using PassThePigs.Services.Interfaces;
using PassThePigs.Domain;

namespace PassThePigs.Hub.Hubs;

public class GameHub : Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IGameOrchestrationService _gameOrchestrationService;

    public GameHub(IGameOrchestrationService gameOrchestrationService)
    {
        _gameOrchestrationService = gameOrchestrationService;
    }

    public async Task CreateGame()
    {
        var gameId = await _gameOrchestrationService.CreateGame();
        await Clients.Caller.SendAsync("GameCreated", gameId);
    }

    public async Task UpdateGame(GameStateModel gameStateModel)
    {
        var updatedState = await _gameOrchestrationService.UpdateGame(gameStateModel);
        await Clients.Group(updatedState.GameId.ToString()).SendAsync("GameUpdated", updatedState);
    }

    public async Task GetGameState(Guid gameId)
    {
        var gameState = await _gameOrchestrationService.GetGameState(gameId);
        if (gameState != null)
        {
            await Clients.Group(gameState.GameId.ToString()).SendAsync("GameStateRetrieved", gameState);
        }
    }

    public async Task JoinGame(Guid gameId, string playerName)
    {
        await _gameOrchestrationService.JoinGame(gameId, playerName);
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined");
    }

    public async Task RollPigs(GameStateModel gameStateModel)
    {
        var updatedState = await _gameOrchestrationService.RollPigs(gameStateModel);
        await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("RollPigs", updatedState);
    }

    public async Task BankPoints(GameStateModel gameStateModel)
    {
        var updatedState = await _gameOrchestrationService.BankPoints(gameStateModel);
        await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("BankPoints", updatedState);
    }
}



// using Microsoft.AspNetCore.SignalR;
// using PassThePigs.Services.Interfaces;
// using PassThePigs.Domain;

// namespace PassThePigs.Hub.Hubs;

// public class GameHub : Microsoft.AspNetCore.SignalR.Hub
// {
//     private readonly IGameCacheService _cacheService;
//     private readonly IGameLogicService _gameLogicService;

//     public GameHub(IGameCacheService cacheService, IGameLogicService gameLogicService)
//     {
//         _cacheService = cacheService;
//         _gameLogicService = gameLogicService;
//     }

//     public async Task CreateGame()
//     {
//         var gameState = _cacheService.CreateGame();
//         _cacheService.SaveGameState(gameState.GameId, gameState);
//         await Clients.Caller.SendAsync("GameCreated", gameState.GameId);
//     }

//     public async Task UpdateGame(GameStateModel gameStateModel)
//     {
//         _cacheService.SaveGameState(gameStateModel.GameId, gameStateModel);
//         await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("GameUpdated", gameStateModel);
//     }

//     public async Task GetGameState(Guid gameId)
//     {
//         var gameState = _cacheService.GetGameState(gameId);
//         if (gameState != null)
//         {
//             await Clients.Group(gameState.GameId.ToString()).SendAsync("GameStateRetrieved", gameState);
//         }
//     }

//     public async Task JoinGame(Guid gameId, string playerName)
//     {
//         var gameState = _cacheService.GetGameState(gameId);

//         // handle player reconnection
//         var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
//         if (player != null)
//         {
//             player.IsConnected = true;
//             _cacheService.SaveGameState(gameId, gameState);
//         }
//         else
//         {
//             _gameLogicService.AddPlayer(gameId, playerName);
//             var updatedState = _cacheService.GetGameState(gameId);
//             _cacheService.SaveGameState(gameId, updatedState);
//         }

//         await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
//         await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", playerName);
//     }

//     public async Task RollPigs(GameStateModel gameStateModel)
//     {
//         var updatedState = _gameLogicService.PlayerRolls(gameStateModel);
//         _cacheService.SaveGameState(updatedState.GameId, updatedState);
//         await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("RollPigs", updatedState);
//     }

//     public async Task BankPoints(GameStateModel gameStateModel)
//     {
//         var updatedState = _gameLogicService.PlayerBanks(gameStateModel);
//         _cacheService.SaveGameState(updatedState.GameId, updatedState);
//         await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("BankPoints", updatedState);
//     }
// }
