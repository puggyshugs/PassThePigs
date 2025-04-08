namespace PassThePigs.Domain;

public class PlayerChatModel
{
    public Guid GameId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string PlayerName { get; set; } = string.Empty;
    public bool IsSender { get; set; }
}
