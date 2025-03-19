using Microsoft.AspNetCore.SignalR;
using PassThePigs.Services.Interfaces;
using PassThePigs.Domain;

namespace PassThePigs.Hub.Hubs;

public class GameHub : Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IGameCacheService _cacheService;
    private readonly IGameLogicService _gameLogicService;

    public GameHub(IGameCacheService cacheService, IGameLogicService gameLogicService)
    {
        _cacheService = cacheService;
        _gameLogicService = gameLogicService;
    }

    public async Task CreateGame()
    {
        var gameState = _cacheService.CreateGame();
        _cacheService.SaveGameState(gameState.GameId, gameState);
        await Clients.Caller.SendAsync("GameCreated", gameState.GameId);
    }

    public async Task JoinGame(Guid gameId, string playerName)
    {
        var gameState = _cacheService.GetGameState(gameId);

        // handle player reconnection
        var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
        if (player != null)
        {
            player.IsConnected = true;
            _cacheService.SaveGameState(gameId, gameState);
        }
        else
        {
            _gameLogicService.AddPlayer(gameId, playerName);
            var updatedState = _cacheService.GetGameState(gameId);
            _cacheService.SaveGameState(gameId, updatedState);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", playerName);
    }

    public async Task RollPigs(GameStateModel gameStateModel)
    {
        var updatedState = _gameLogicService.PlayerRolls(gameStateModel);
        _cacheService.SaveGameState(updatedState.GameId, updatedState);
        await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("RollPigs", updatedState);
    }

    public async Task BankPoints(GameStateModel gameStateModel)
    {
        var updatedState = _gameLogicService.PlayerBanks(gameStateModel);
        _cacheService.SaveGameState(updatedState.GameId, updatedState);
        await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("BankPoints", updatedState);
    }

    public async Task GetGameState(Guid gameId)
    {
        var gameState = _cacheService.GetGameState(gameId);
        if (gameState != null)
        {
            await Clients.Group(gameState.GameId.ToString()).SendAsync("GameStateRetrieved", gameState);
        }
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

//     public async Task JoinGame(Guid gameId, string playerName)
//     {
//         var gameState = _cacheService.GetGameState(gameId);

//         // handle player reconnection
//         var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
//         if (player != null)
//         {
//             player.IsConnected = true;
//         }
//         else
//         {
//             _gameLogicService.AddPlayer(gameId, playerName);
//         }

//         await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
//         await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", playerName);
//     }


//     public async Task RollPigs(GameStateModel gameStateModel)
//     {
//         _gameLogicService.PlayerRolls(gameStateModel);
//         var updatedState = _cacheService.GetGameState(gameStateModel.GameId);
//         await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("RollPigs", updatedState);
//     }

//     public async Task BankPoints(GameStateModel gameStateModel)
//     {
//         var updatedState = _gameLogicService.PlayerBanks(gameStateModel);
//         await Clients.Group(gameStateModel.GameId.ToString()).SendAsync("BankPoints", updatedState);
//     }

//     public async Task GetGameState(Guid gameId)
//     {
//         var gameState = _cacheService.GetGameState(gameId);
//         if (gameState != null)
//         {
//             await Clients.Caller.SendAsync("GameStateRetrieved", gameState);
//         }
//     }

// }
