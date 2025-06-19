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
        else
        {
            await Clients.Caller.SendAsync("GameNotFound", $"Game with ID {gameId} not found.");
        }
    }

    public async Task JoinGame(Guid gameId, string playerName)
    {
        var gameState = await _gameOrchestrationService.JoinGame(gameId, playerName);
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        await Clients.Group(gameId.ToString()).SendAsync("PlayerJoined", gameState);
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
