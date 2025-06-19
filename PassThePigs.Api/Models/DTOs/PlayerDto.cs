namespace PassThePigs.Api.Models.DTOs;

public class PlayerDto
{
    public Guid PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int TotalScore { get; set; }
    public int RoundScore { get; set; }
}
