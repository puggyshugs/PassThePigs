using PassThePigs.Domain;

namespace PassThePigs.Services.Helpers
{
    public class PlayerLogicHelper
    {
        private readonly int _maxPlayers = 6;

        public bool AddPlayer(ref GameStateModel gameState, string playerName)
        {
            if (gameState.Players.Count >= _maxPlayers)
                return false; // Game is full

            if (gameState.Players.Any(p => p.PlayerName == playerName))
                return false; // Prevent duplicate names

            gameState.Players.Add(new PlayerModel { PlayerId = Guid.NewGuid(), PlayerName = playerName, Score = 0 });

            // If first player, set as current turn
            if (gameState.Players.Count == 1)
                gameState.CurrentTurnIndex = 0;

            return true;
        }

        public bool RemovePlayer(GameStateModel gameState)
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerId == gameState.CurrentPlayerId);
            if (player != null)
            {
                int playerIndex = gameState.Players.IndexOf(player);
                gameState.Players.Remove(player);

                // Adjust turn index
                if (gameState.Players.Count > 0)
                {
                    if (gameState.CurrentTurnIndex >= gameState.Players.Count)
                        gameState.CurrentTurnIndex = 0;
                }
                return true;
            }
            return false;
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
