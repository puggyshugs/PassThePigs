using System;

namespace PassThePigs.Services.Cache.CacheModels;

public class GameStateModel
{
    public Guid GameId { get; set; }
    public List<PlayerModel> Players { get; set; } = new();
}
