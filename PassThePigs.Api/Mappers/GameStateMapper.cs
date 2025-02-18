using PassThePigs.Api.Models.DTOs;
using PassThePigs.Domain;

namespace PassThePigsApi.Mappers;

public static class GameStateMapper
{
    public static GameStateDto ToDto(GameStateModel model)
    {
        return new GameStateDto
        {
            GameId = model.GameId,
            Players = model.Players.Select(p => new PlayerDto
            {
                PlayerName = p.PlayerName,
                Score = p.Score
            }).ToList(),
            CurrentTurnIndex = model.CurrentTurnIndex,
            IsGameOver = model.IsGameOver
        };
    }
}

