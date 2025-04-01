using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using PassThePigs.Domain;
using PassThePigs.Hub.Hubs;
using PassThePigs.Services.Interfaces;

namespace PassThePigs.Hub.Tests.GameHubTests;

public class GameHubTests
{
    private readonly IGameOrchestrationService _mockGameOrchestrationService;
    private readonly IHubCallerClients _mockClients;
    private readonly ISingleClientProxy _mockClientProxy;
    private readonly IClientProxy _mockGroupProxy;
    private readonly HubCallerContext _mockContext;
    private readonly IGroupManager _mockGroups;
    private readonly GameHub _hub;

    public GameHubTests()
    {
        _mockGameOrchestrationService = Substitute.For<IGameOrchestrationService>();
        _mockClients = Substitute.For<IHubCallerClients>();
        _mockClientProxy = Substitute.For<ISingleClientProxy>();
        _mockGroupProxy = Substitute.For<IClientProxy>();
        _mockContext = Substitute.For<HubCallerContext>();
        _mockGroups = Substitute.For<IGroupManager>();

        _mockClients.Caller.Returns(_mockClientProxy);
        _mockClients.Group(Arg.Any<string>()).Returns(_mockGroupProxy);

        _hub = new GameHub(_mockGameOrchestrationService)
        {
            Clients = _mockClients,
            Context = _mockContext,
            Groups = _mockGroups
        };
    }

    [Fact]
    public async Task CreateGame_ShouldCallOrchestrationServiceAndNotifyClient()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        _mockGameOrchestrationService.CreateGame().Returns(Task.FromResult(gameId));

        // Act
        await _hub.CreateGame();

        // Assert
        await _mockClientProxy.Received(1).SendCoreAsync(
            "GameCreated",
            Arg.Is<object[]>(args => args.Length == 1 && args[0].Equals(gameId)),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task JoinGame_ShouldAddPlayerAndNotifyGroup()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var playerName = "TestPlayer";
        _mockContext.ConnectionId.Returns("123");

        // Act
        await _hub.JoinGame(gameId, playerName);

        // Assert
        await _mockGroups.Received(1).AddToGroupAsync("123", gameId.ToString(), Arg.Any<CancellationToken>());
        await _mockGroupProxy.Received(1).SendCoreAsync(
            "PlayerJoined",
            Arg.Any<object[]>(),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task RollPigs_ShouldUpdateGameStateAndNotifyGroup()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameStateModel = new GameStateModel { GameId = gameId };
        var updatedState = new GameStateModel { GameId = gameId };
        _mockGameOrchestrationService.RollPigs(gameStateModel).Returns(Task.FromResult(updatedState));

        // Act
        await _hub.RollPigs(gameStateModel);

        // Assert
        await _mockGroupProxy.Received(1).SendCoreAsync(
            "RollPigs",
            Arg.Is<object[]>(args => args.Length == 1 && args[0].Equals(updatedState)),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task BankPoints_ShouldUpdateGameStateAndNotifyGroup()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameStateModel = new GameStateModel { GameId = gameId };
        var updatedState = new GameStateModel { GameId = gameId };
        _mockGameOrchestrationService.BankPoints(gameStateModel).Returns(Task.FromResult(updatedState));

        // Act
        await _hub.BankPoints(gameStateModel);

        // Assert
        await _mockGroupProxy.Received(1).SendCoreAsync(
            "BankPoints",
            Arg.Is<object[]>(args => args.Length == 1 && args[0].Equals(updatedState)),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetGameState_ShouldSendGameStateToGroup()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameState = new GameStateModel { GameId = gameId };
        _mockGameOrchestrationService.GetGameState(gameId).Returns(Task.FromResult(gameState));

        // Act
        await _hub.GetGameState(gameId);

        // Assert
        await _mockGroupProxy.Received(1).SendCoreAsync(
            "GameStateRetrieved",
            Arg.Is<object[]>(args => args.Length == 1 && args[0].Equals(gameState)),
            Arg.Any<CancellationToken>()
        );
    }
}
