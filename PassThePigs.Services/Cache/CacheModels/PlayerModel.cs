namespace PassThePigs.Services.Cache.CacheModels;

public class PlayerModel
{
    public string PlayerName { get; set; } = string.Empty;
    public int Score { get; set; }
    public bool HasWon { get; set; }
}
