using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;
using PassThePigs.Services.Services;

namespace PassThePigs.Services.Tests.GameCacheServiceTests;
public class GameCacheServiceTests
{
    private readonly IGameMemoryCache _gameMemoryCache;
    private readonly GameCacheService _gameCacheService;

    public GameCacheServiceTests()
    {
        _gameMemoryCache = Substitute.For<IGameMemoryCache>();
        _gameCacheService = new GameCacheService(_gameMemoryCache);
    }

    [Fact]
    public void CreateGame_ShouldReturnNewGameState()
    {
        // Act
        var result = _gameCacheService.CreateGame();

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.GameId);
        Assert.Contains("successfully created", result.Message);
        _gameMemoryCache.Received(1).CreateGame(result.GameId, result);
    }

    [Fact]
    public void SaveGameState_ShouldUpdateAndReturnGameState()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameState = new GameStateModel { GameId = gameId };

        // Act
        var result = _gameCacheService.SaveGameState(gameId, gameState);

        // Assert
        Assert.Equal(gameId, result.GameId);
        Assert.Contains("successfully saved", result.Message);
        _gameMemoryCache.Received(1).UpdateGame(gameId, gameState);
    }

    [Fact]
    public void GetGameState_WhenGameExists_ShouldReturnExistingState()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var existingState = new GameStateModel { GameId = gameId };
        _gameMemoryCache.GetGameState(gameId).Returns(existingState);

        // Act
        var result = _gameCacheService.GetGameState(gameId);

        // Assert
        Assert.Equal(gameId, result.GameId);
        Assert.Contains("successfully retrieved", result.Message);
        _gameMemoryCache.Received(1).GetGameState(gameId);
    }

    [Fact]
    public void GetGameState_WhenGameDoesNotExist_ShouldCreateNewGame()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _gameMemoryCache.GetGameState(gameId).Returns(_gameCacheService.CreateGame());

        // Act
        var result = _gameCacheService.GetGameState(gameId);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.GameId);
        Assert.Contains("successfully created", result.Message);
    }

    [Fact]
    public void RemoveGameState_ShouldRemoveGameAndReturnEmptyGameState()
    {
        // Arrange
        var gameId = Guid.NewGuid();

        // Act
        var result = _gameCacheService.RemoveGameState(gameId);

        // Assert
        Assert.Equal(Guid.Empty, result.GameId);
        Assert.Contains("successfully removed", result.Message);
        _gameMemoryCache.Received(1).EndGame(gameId);
    }
}
