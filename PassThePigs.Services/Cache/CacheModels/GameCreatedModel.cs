namespace PassThePigs.Services.Cache.CacheModels;

public class Game
{
    public string Message { get; set; } = string.Empty;
    public Guid GameId { get; set; } = Guid.Empty;
}
