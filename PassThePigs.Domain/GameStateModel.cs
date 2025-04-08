using System;

namespace PassThePigs.Domain;

public class GameStateModel
{
    public Guid GameId { get; set; }
    public string JoiningCode { get; set; } = string.Empty;
    public string Message { get; set; }
    public List<PlayerModel> Players { get; set; } = [];
    public int CurrentTurnIndex { get; set; }
    public bool IsGameOver { get; set; }
    public Guid CurrentPlayerId { get; set; }
    public List<PlayerChatModel> PlayerChat { get; set; } = [];
}
