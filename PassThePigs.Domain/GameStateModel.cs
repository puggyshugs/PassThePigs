using System;

namespace PassThePigs.Domain;

public class GameStateModel
{
    public Guid GameId { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<PlayerModel> Players { get; set; } = [];
    public int CurrentTurnIndex { get; set; }
}
