using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using PassThePigs.Api.Controllers;
using PassThePigs.Services.Interfaces;
using PassThePigs.Domain;
using PassThePigs.Api.Models.DTOs;

namespace PassThePigs.Api.Tests.GameControllerTests;

public class GameControllerTests
{
    private readonly GameController _controller;
    private readonly IGameCacheService _gameCacheService = Substitute.For<IGameCacheService>();

    public GameControllerTests()
    {
        _controller = new GameController(_gameCacheService);
    }

    [Fact]
    public async Task GetGameState_ReturnsNotFound_WhenGameDoesNotExist()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _gameCacheService.GetGameState(gameId).Returns((GameStateModel)null);

        // Act
        var result = await _controller.GetGameState(gameId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("No active game found.", notFoundResult.Value);
    }

    [Fact]
    public async Task GetGameState_ReturnsGameState_WhenGameExists()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameState = new GameStateModel { GameId = gameId };
        _gameCacheService.GetGameState(gameId).Returns(gameState);

        // Act
        var result = await _controller.GetGameState(gameId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGameState = Assert.IsType<GameStateDto>(okResult.Value);
        Assert.Equal(gameId, returnedGameState.GameId);
    }

    [Fact]
    public async Task CreateGame_ReturnsCreatedGame()
    {
        // Arrange
        var gameState = new GameStateModel { GameId = Guid.NewGuid() };
        _gameCacheService.CreateGame().Returns(gameState);

        // Act
        var result = await _controller.CreateGame();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedGameState = Assert.IsType<GameStateDto>(okResult.Value);
        Assert.Equal(gameState.GameId, returnedGameState.GameId);
    }

    [Fact]
    public void EndGame_RemovesGameAndReturnsOk()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameCacheService = Substitute.For<IGameCacheService>();
        var controller = new GameController(gameCacheService);

        // Act
        var result = controller.EndGame(gameId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var responseValue = okResult.Value;

        Assert.NotNull(responseValue);
        Assert.Contains($"Game: {gameId} ended!", responseValue.ToString());
    }

}
