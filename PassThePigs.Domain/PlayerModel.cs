using System;

namespace PassThePigs.Domain;

public class PlayerModel
{
    public string PlayerName { get; set; } = string.Empty;
    public Guid PlayerId { get; set; }
    public int Score { get; set; }
    public bool HasWon { get; set; }
    public bool IsConnected { get; set; }
    public bool TurnOver { get; set; }
}
