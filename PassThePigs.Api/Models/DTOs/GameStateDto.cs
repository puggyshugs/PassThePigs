using System;

namespace PassThePigs.Api.Models.DTOs;

public class GameStateDto
{
    public Guid GameId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string JoiningCode { get; set; } = string.Empty;
    public List<PlayerDto> Players { get; set; } = new();
    public int CurrentTurnIndex { get; set; }
    public bool IsGameOver { get; set; }
}
