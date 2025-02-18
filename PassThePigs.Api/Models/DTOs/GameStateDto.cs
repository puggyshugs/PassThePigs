using System;

namespace PassThePigs.Api.Models.DTOs;

public class GameStateDto
{
    public Guid GameId { get; set; }
    public List<PlayerDto> Players { get; set; } = new();
    public int CurrentTurnIndex { get; set; }
    public bool IsGameOver { get; set; }
}
