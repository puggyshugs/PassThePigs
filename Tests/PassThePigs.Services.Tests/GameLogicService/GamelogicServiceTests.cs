using PassThePigs.GameLogic.Services;
using PassThePigs.Data.Cache.Interfaces;
using PassThePigs.Domain;
using PassThePigs.Services.Helpers.Interfaces;
using PassThePigs.Services.Interfaces;
using PassThePigs.Services.Helpers.Enums;
using PassThePigs.Services.Helpers;

namespace PassThePigs.Services.Tests.GameCacheServiceTests;

public class GameLogicServiceTests
{
    private readonly IGameCacheService _gameMemoryCache;
    private readonly IPlayerLogicHelper _playerLogicHelper;
    private readonly IGameLogicService _gameLogicService;
    private readonly IPigThrowLogicHelper _pigThrowLogicHelper;

    public GameLogicServiceTests()
    {
        _gameMemoryCache = Substitute.For<IGameCacheService>();
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
        var helper = new PlayerLogicHelper();
        helper.AddPlayer(gameState, "Player2");

        // Act
        var result = _gameLogicService.AddPlayer(gameId, "Player2");

        // Assert
        Assert.Equal("PlayerAddedSuccessfully", result.Message);
        _gameMemoryCache.Received(1).SaveGameState(gameId, gameState);
    }

    [Fact]
    public void AddPlayer_WhenGameDoesNotExist_ShouldReturnAnEmptyGameStateModel()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _gameMemoryCache.GetGameState(gameId).Returns((GameStateModel)null);

        // Act
        var result = _gameLogicService.AddPlayer(gameId, "Player1");

        // Assert
        Assert.Equal(result.GameId, Guid.Empty);
    }

    [Fact]
    public void RemovePlayer_Helper_ShouldRemovePlayerFromGameState()
    {
        var gameState = new GameStateModel
        {
            GameId = Guid.NewGuid(),
            Players = new List<PlayerModel>
        {
            new PlayerModel { PlayerId = Guid.NewGuid(), PlayerName = "Player1", Score = 0 },
            new PlayerModel { PlayerId = Guid.NewGuid(), PlayerName = "Player2", Score = 0 }
        }
        };

        var helper = new PlayerLogicHelper();
        helper.RemovePlayer(gameState, "Player1");

        Assert.Single(gameState.Players);
        Assert.DoesNotContain(gameState.Players, p => p.PlayerName == "Player1");
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
        _gameMemoryCache.Received(1).SaveGameState(gameState.GameId, gameState);
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
