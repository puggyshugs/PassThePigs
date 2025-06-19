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
            Message = model.Message,
            JoiningCode = model.JoiningCode,
            Players = model.Players.Select(p => new PlayerDto
            {
                PlayerId = p.PlayerId,
                PlayerName = p.PlayerName,
                TotalScore = p.TotalScore,
                RoundScore = p.Score
            }).ToList(),
            CurrentTurnIndex = model.CurrentTurnIndex,
            IsGameOver = model.IsGameOver
        };
    }
}
