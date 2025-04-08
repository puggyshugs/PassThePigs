using PassThePigs.Domain;
using PassThePigs.Services.Helpers.Interfaces;
using PassThePigs.Services.Helpers.Enums;
using System.Linq;
namespace PassThePigs.Services.Helpers
{
    public class PlayerLogicHelper : IPlayerLogicHelper
    {
        private readonly int _maxPlayers = 6;

        public void AddPlayer(GameStateModel gameState, string playerName)
        {
            if (gameState.Players.Count >= _maxPlayers)
                gameState.Message = AddPlayerMessages.FullGame.ToString(); // Game is full

            if (gameState.Players.Any(p => p.PlayerName == playerName))
                gameState.Message = AddPlayerMessages.DuplicatePlayerName.ToString(); // Prevent duplicate names
            else
            {
                gameState.Players.Add(new PlayerModel { PlayerId = Guid.NewGuid(), PlayerName = playerName, Score = 0 });
                gameState.Message = AddPlayerMessages.PlayerAddedSuccessfully.ToString(); // Player added successfully

                // If first player, set as current turn
                if (gameState.Players.Count == 1)
                    gameState.CurrentTurnIndex = 0;
            }
        }

        public void RemovePlayer(GameStateModel gameState, string playerName)
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerName.ToLower() == playerName.ToLower());
            if (player == null)
            {
                gameState.Message = RemovePlayerMessages.PlayerNotFound.ToString();
                return;
            }
            gameState.Players.Remove(player);
            gameState.Message = RemovePlayerMessages.PlayerRemovedSuccessfully.ToString();

            if (gameState.Players.Count > 0)
            {
                if (gameState.CurrentTurnIndex >= gameState.Players.Count)
                    gameState.CurrentTurnIndex = 0;
            }
        }

        public void EndTurn(GameStateModel gameState)
        {
            if (gameState.Players.Count > 0)
            {
                gameState.CurrentTurnIndex = (gameState.CurrentTurnIndex + 1) % gameState.Players.Count;
            }
        }

        // probably not needed
        public void HandlePlayerReconnection(GameStateModel gameState, string playerName)
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
            if (player != null)
            {
                player.IsConnected = true;
            }
        }

        // also probably not needed
        public void HandlePlayerDisconnection(GameStateModel gameState, string playerName)
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
            if (player != null)
            {
                player.IsConnected = false;
            }
        }
    }
}
