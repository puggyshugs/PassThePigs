using PassThePigs.GameLogic.Services;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;
using PassThePigs.Services.Helpers.Interfaces;
using PassThePigs.Services.Interfaces;

namespace PassThePigs.Services.Tests.GameCacheServiceTests;

public class GameLogicServiceTests
{
    private readonly IGameMemoryCache _gameMemoryCache;
    private readonly IPlayerLogicHelper _playerLogicHelper;
    private readonly IGameLogicService _gameLogicService;
    private readonly IPigThrowLogicHelper _pigThrowLogicHelper;

    public GameLogicServiceTests()
    {
        _gameMemoryCache = Substitute.For<IGameMemoryCache>();
        _pigThrowLogicHelper = Substitute.For<IPigThrowLogicHelper>();
        _playerLogicHelper = Substitute.For<IPlayerLogicHelper>();
        _gameLogicService = new GameLogicService(_gameMemoryCache, _playerLogicHelper, _pigThrowLogicHelper);
    }

    [Fact]
    public void AddPlayer_WhenGameExists_ShouldAddPlayerAndUpdateCache()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameState = new GameStateModel
        {
            GameId = gameId,
            Players = new List<PlayerModel>()
            {
                new PlayerModel { PlayerId = Guid.NewGuid(), PlayerName = "Player1", Score = 0 }
            }
        };
        _gameMemoryCache.GetGameState(gameId).Returns(gameState);
        _playerLogicHelper.AddPlayer(ref gameState, gameState.Players.FirstOrDefault().PlayerName).Returns(true);

        // Act
        var result = _gameLogicService.AddPlayer(gameId, "Player1");

        // Assert
        Assert.True(result);
        _gameMemoryCache.Received(1).UpdateGame(gameId, gameState);
    }

    [Fact]
    public void AddPlayer_WhenGameDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _gameMemoryCache.GetGameState(gameId).Returns((GameStateModel)null);

        // Act
        var result = _gameLogicService.AddPlayer(gameId, "Player1");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RemovePlayer_ShouldCallRemoveAndUpdateCache()
    {
        // Arrange
        var gameState = new GameStateModel { GameId = Guid.NewGuid() };
        _playerLogicHelper.RemovePlayer(gameState).Returns(true);

        // Act
        var result = _gameLogicService.RemovePlayer(gameState);

        // Assert
        Assert.True(result);
        _gameMemoryCache.Received(1).UpdateGame(gameState.GameId, gameState);
    }

    [Fact]
    public void PlayerRolls_WhenPlayerExists_ShouldUpdateGameState()
    {
        // Arrange
        var player = new PlayerModel { PlayerId = Guid.NewGuid(), Score = 10, HasWon = false, PlayerName = "Player1", TurnOver = false };
        var gameState = new GameStateModel
        {
            GameId = Guid.NewGuid(),
            CurrentPlayerId = player.PlayerId,
            Players = new[] { player }.ToList()
        };

        // Act
        var result = _gameLogicService.PlayerRolls(gameState);

        // Assert
        Assert.NotNull(result);
        _gameMemoryCache.Received(1).UpdateGame(gameState.GameId, gameState);
    }

    [Fact]
    public void PlayerBanks_WhenGameStateIsNull_ShouldReturnErrorMessage()
    {
        // Act
        var result = _gameLogicService.PlayerBanks(null);

        // Assert
        Assert.Equal("Game state not found", result.Message);
    }

    [Fact]
    public void PlayerBanks_WhenPlayerNotFound_ShouldReturnErrorMessage()
    {
        // Arrange
        var gameState = new GameStateModel { GameId = Guid.NewGuid(), CurrentPlayerId = Guid.NewGuid() };

        // Act
        var result = _gameLogicService.PlayerBanks(gameState);

        // Assert
        Assert.Equal("Player not found", result.Message);
    }
}
