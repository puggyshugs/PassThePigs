using System;

namespace PassThePigs.Api.Models.DTOs;

public class PlayerDto
{
    public string PlayerName { get; set; } = string.Empty;
    public int Score { get; set; }
}
