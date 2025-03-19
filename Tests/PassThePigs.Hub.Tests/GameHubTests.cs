using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using PassThePigs.Domain;
using PassThePigs.Hub.Hubs;
using PassThePigs.Services.Interfaces;
using Xunit;

namespace PassThePigs.Hub.Tests.GameHubTests;

public class GameHubTests
{
    private readonly IGameCacheService _mockCacheService;
    private readonly IGameLogicService _mockGameLogicService;
    private readonly IHubCallerClients _mockClients;
    private readonly ISingleClientProxy _mockClientProxy;
    private readonly IClientProxy _mockGroupProxy;
    private readonly HubCallerContext _mockContext;
    private readonly IGroupManager _mockGroups;
    private readonly GameHub _hub;

    public GameHubTests()
    {
        _mockCacheService = Substitute.For<IGameCacheService>();
        _mockGameLogicService = Substitute.For<IGameLogicService>();
        _mockClients = Substitute.For<IHubCallerClients>();
        _mockClientProxy = Substitute.For<ISingleClientProxy>();
        _mockGroupProxy = Substitute.For<IClientProxy>();
        _mockContext = Substitute.For<HubCallerContext>();
        _mockGroups = Substitute.For<IGroupManager>();

        _mockClients.Caller.Returns(_mockClientProxy);
        _mockClients.Group(Arg.Any<string>()).Returns(_mockGroupProxy);

        _hub = new GameHub(_mockCacheService, _mockGameLogicService)
        {
            Clients = _mockClients,
            Context = _mockContext,
            Groups = _mockGroups
        };
    }

    [Fact]
    public async Task CreateGame_ShouldCallCacheServiceAndNotifyClient()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameState = new GameStateModel { GameId = gameId };
        _mockCacheService.CreateGame().Returns(gameState);

        // Act
        await _hub.CreateGame();

        // Assert
        _mockCacheService.Received(1).SaveGameState(gameId, gameState);
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
        var gameState = new GameStateModel { GameId = gameId, Players = new List<PlayerModel>() };
        var updatedState = new GameStateModel { GameId = gameId, Players = new List<PlayerModel> { new PlayerModel { PlayerName = playerName } } };

        _mockCacheService.GetGameState(gameId).Returns(gameState);
        _mockContext.ConnectionId.Returns("123");
        _mockGameLogicService.AddPlayer(gameId, playerName).Returns(true);

        // Act
        await _hub.JoinGame(gameId, playerName);

        // Assert
        _mockGameLogicService.Received(1).AddPlayer(gameId, playerName);
        await _mockGroups.Received(1).AddToGroupAsync("123", gameId.ToString(), Arg.Any<CancellationToken>());
        await _mockGroupProxy.Received(1).SendCoreAsync(
            "PlayerJoined",
            Arg.Is<object[]>(args => args.Length == 1 && args[0].Equals(playerName)),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task RollPigs_ShouldUpdateGameStateAndNotifyGroup()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameState = new GameStateModel
        {
            GameId = gameId,
            Players = new List<PlayerModel> { new PlayerModel { PlayerName = "TestPlayer", TurnOver = false } },
        };
        var updatedState = new GameStateModel
        {
            GameId = gameId,
            Players = new List<PlayerModel> { new PlayerModel { PlayerName = "TestPlayer", TurnOver = true } },
        };

        _mockGameLogicService.PlayerRolls(gameState).Returns(updatedState);
        _mockCacheService.GetGameState(gameId).Returns(updatedState);

        // Act
        await _hub.RollPigs(gameState);

        // Assert
        _mockGameLogicService.Received(1).PlayerRolls(gameState);
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
        var gameState = new GameStateModel { GameId = gameId };
        var updatedState = new GameStateModel { GameId = gameId, CurrentTurnIndex = 0 };

        _mockGameLogicService.PlayerBanks(gameState).Returns(updatedState);

        // Act
        await _hub.BankPoints(gameState);

        // Assert
        await _mockGroupProxy.Received(1).SendCoreAsync(
            "BankPoints",
            Arg.Is<object[]>(args => args.Length == 1 && args[0].Equals(updatedState)),
            Arg.Any<CancellationToken>()
        );
    }

    [Fact]
    public async Task GetGameState_ShouldSendGameStateToCaller()
    {
        // Arrange
        var gameId = Guid.NewGuid();
        var gameState = new GameStateModel { GameId = gameId };
        _mockCacheService.GetGameState(gameId).Returns(gameState);

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
///////////////////////////////////////////////////////////////
// The following is written using Moq not NSubstitute. 
// But I've chosen NSubstitute for readability and to keep the codebase consistent.

// using Microsoft.AspNetCore.SignalR;
// using Moq;
// using PassThePigs.Domain;
// using PassThePigs.Hub.Hubs;
// using PassThePigs.Services.Interfaces;
// using Xunit;

// namespace PassThePigs.Hub.Tests.GameHubTests;

// public class GameHubTests
// {
//     private readonly Mock<IGameCacheService> _mockCacheService;
//     private readonly Mock<IGameLogicService> _mockGameLogicService;
//     private readonly Mock<IHubCallerClients> _mockClients;
//     private readonly Mock<ISingleClientProxy> _mockClientProxy;
//     private readonly Mock<IClientProxy> _mockGroupProxy;
//     private readonly Mock<HubCallerContext> _mockContext;
//     private readonly Mock<IGroupManager> _mockGroups;
//     private readonly GameHub _hub;

//     public GameHubTests()
//     {
//         _mockCacheService = new Mock<IGameCacheService>();
//         _mockGameLogicService = new Mock<IGameLogicService>();
//         _mockClients = new Mock<IHubCallerClients>();
//         _mockClientProxy = new Mock<ISingleClientProxy>();
//         _mockGroupProxy = new Mock<IClientProxy>();
//         _mockContext = new Mock<HubCallerContext>();
//         _mockGroups = new Mock<IGroupManager>();

//         _mockClients.Setup(clients => clients.Caller).Returns(_mockClientProxy.Object);
//         _mockClients.Setup(clients => clients.Group(It.IsAny<string>())).Returns(_mockGroupProxy.Object);

//         _hub = new GameHub(_mockCacheService.Object, _mockGameLogicService.Object)
//         {
//             Clients = _mockClients.Object,
//             Context = _mockContext.Object,
//             Groups = _mockGroups.Object
//         };
//     }

//     [Fact]
//     public async Task CreateGame_ShouldCallCacheServiceAndNotifyClient()
//     {
//         // Arrange
//         var gameId = Guid.NewGuid();
//         var gameState = new GameStateModel { GameId = gameId };
//         _mockCacheService.Setup(s => s.CreateGame()).Returns(gameState);

//         // Act
//         await _hub.CreateGame();

//         // Assert
//         _mockCacheService.Verify(s => s.SaveGameState(gameId, gameState), Times.Once);
//         _mockClientProxy.Verify(
//             c => c.SendCoreAsync(
//                 "GameCreated",
//                 It.Is<object[]>(args => args.Length == 1 && args[0].Equals(gameId)),
//                 It.IsAny<CancellationToken>()
//             ),
//             Times.Once
//         );
//     }

//     [Fact]
//     public async Task JoinGame_ShouldAddPlayerAndNotifyGroup()
//     {
//         // Arrange
//         var gameId = Guid.NewGuid();
//         var playerName = "TestPlayer";
//         var gameState = new GameStateModel { GameId = gameId, Players = new List<PlayerModel>() };
//         var updatedState = new GameStateModel { GameId = gameId, Players = new List<PlayerModel> { new PlayerModel { PlayerName = playerName } } };

//         _mockCacheService.Setup(s => s.GetGameState(gameId)).Returns(gameState);
//         _mockContext.Setup(c => c.ConnectionId).Returns("123");
//         _mockGameLogicService.Setup(s => s.AddPlayer(gameId, playerName)).Returns(true);

//         // Act
//         await _hub.JoinGame(gameId, playerName);

//         // Assert
//         _mockGameLogicService.Verify(s => s.AddPlayer(gameId, playerName), Times.Once);
//         _mockGroups.Verify(g => g.AddToGroupAsync("123", gameId.ToString(), It.IsAny<CancellationToken>()), Times.Once);
//         _mockGroupProxy.Verify(
//             g => g.SendCoreAsync(
//                 "PlayerJoined",
//                 It.Is<object[]>(args => args.Length == 1 && args[0].Equals(playerName)),
//                 It.IsAny<CancellationToken>()
//             ),
//             Times.Once
//         );
//     }

//     [Fact]
//     public async Task RollPigs_ShouldUpdateGameStateAndNotifyGroup()
//     {
//         // Arrange
//         var gameId = Guid.NewGuid();
//         var gameState = new GameStateModel
//         {
//             GameId = gameId,
//             Players = new List<PlayerModel> { new PlayerModel { PlayerName = "TestPlayer", TurnOver = false } },
//         };
//         var updatedState = new GameStateModel
//         {
//             GameId = gameId,
//             Players = new List<PlayerModel> { new PlayerModel { PlayerName = "TestPlayer", TurnOver = true } },
//         };

//         _mockGameLogicService.Setup(s => s.PlayerRolls(gameState)).Returns(updatedState);
//         _mockCacheService.Setup(s => s.GetGameState(gameId)).Returns(updatedState);

//         // Act
//         await _hub.RollPigs(gameState);

//         // Assert
//         _mockGameLogicService.Verify(s => s.PlayerRolls(gameState), Times.Once);
//         // Moq doesn't support SendAsync directly
//         //_mockGroupProxy.Verify(g => g.SendAsync("RollPigs", updatedState, It.IsAny<CancellationToken>()), Times.Once);
//         // have to use SendCoreAsync instead, which is supported
//         _mockGroupProxy.Verify(
//             g => g.SendCoreAsync(
//                 "RollPigs",
//                 It.Is<object[]>(args => args.Length == 1 && args[0].Equals(updatedState)),
//                 It.IsAny<CancellationToken>()
//             ),
//             Times.Once
//         );
//     }

//     [Fact]
//     public async Task BankPoints_ShouldUpdateGameStateAndNotifyGroup()
//     {
//         // Arrange
//         var gameId = Guid.NewGuid();
//         var gameState = new GameStateModel { GameId = gameId };
//         var updatedState = new GameStateModel { GameId = gameId, CurrentTurnIndex = 0 };

//         _mockGameLogicService.Setup(s => s.PlayerBanks(gameState)).Returns(updatedState);

//         // Act
//         await _hub.BankPoints(gameState);

//         // Assert
//         //_mockGroupProxy.Verify(g => g.SendAsync("BankPoints", updatedState, It.IsAny<CancellationToken>()), Times.Once);
//         _mockGroupProxy.Verify(
//             g => g.SendCoreAsync(
//                 "BankPoints",
//                 It.Is<object[]>(args => args.Length == 1 && args[0].Equals(updatedState)),
//                 It.IsAny<CancellationToken>()
//             ),
//             Times.Once
//         );
//     }

//     [Fact]
//     public async Task GetGameState_ShouldSendGameStateToCaller()
//     {
//         // Arrange
//         var gameId = Guid.NewGuid();
//         var gameState = new GameStateModel { GameId = gameId };
//         _mockCacheService.Setup(s => s.GetGameState(gameId)).Returns(gameState);

//         // Act
//         await _hub.GetGameState(gameId);

//         // Assert
//         _mockGroupProxy.Verify(
//             g => g.SendCoreAsync(
//                 "GameStateRetrieved",
//                 It.Is<object[]>(args => args.Length == 1 && args[0].Equals(gameState)),
//                 It.IsAny<CancellationToken>()
//             ),
//             Times.Once
//         );
//     }
// }