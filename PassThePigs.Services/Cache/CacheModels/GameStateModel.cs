namespace PassThePigs.Services.Cache.CacheModels;

public class GameStateModel
{
    public Guid GameId { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<PlayerModel> Players { get; set; } = [];
}
