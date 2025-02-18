using System;
using System.Collections.Generic;
using System.Linq;
using PassThePigs.Domain;
using PassThePigs.Services.Cache.CacheModels;

namespace PassThePigs.GameLogic.Helpers
{
    public class PlayerLogicHelper
    {
        private readonly int _maxPlayers = 6;

        public bool AddPlayer(GameStateModel gameState, string playerName)
        {
            if (gameState.Players.Count >= _maxPlayers)
                return false; // Game is full

            if (gameState.Players.Any(p => p.PlayerName == playerName))
                return false; // Prevent duplicate names

            gameState.Players.Add(new PlayerModel { PlayerName = playerName, Score = 0 });

            // If first player, set as current turn
            if (gameState.Players.Count == 1)
                gameState.CurrentTurnIndex = 0;

            return true;
        }

        public void RemovePlayer(GameStateModel gameState, string playerName)
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
            if (player != null)
            {
                int playerIndex = gameState.Players.IndexOf(player);
                gameState.Players.Remove(player);

                // Adjust turn index if needed
                if (gameState.Players.Count > 0)
                {
                    if (gameState.CurrentTurnIndex >= gameState.Players.Count)
                        gameState.CurrentTurnIndex = 0;
                }
            }
        }

        public void HandlePlayerReconnection(GameStateModel gameState, string playerName)
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
            if (player != null)
            {
                player.IsConnected = true;
            }
        }

        public void HandlePlayerDisconnection(GameStateModel gameState, string playerName)
        {
            var player = gameState.Players.FirstOrDefault(p => p.PlayerName == playerName);
            if (player != null)
            {
                player.IsConnected = false;
            }
        }

        public void PassTurn(GameStateModel gameState)
        {
            if (gameState.Players.Count > 0)
            {
                gameState.CurrentTurnIndex = (gameState.CurrentTurnIndex + 1) % gameState.Players.Count;
            }
        }
    }
}
