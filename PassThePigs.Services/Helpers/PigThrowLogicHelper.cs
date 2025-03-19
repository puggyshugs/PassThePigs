using PassThePigs.Domain;
using PassThePigs.Services.Helpers.Interfaces;

namespace PassThePigs.Services.Helpers
{
    public class PigThrowLogicHelper : IPigThrowLogicHelper
    {
        private readonly Random _random = new();

        private readonly Dictionary<string, (int Points, double Probability)> _pigRollProbabilities = new()
        {
            { "Pig Out", (0, 0.30) },
            { "Sider", (1, 0.25) },
            { "Trotter", (5, 0.10) },
            { "Double Trotter", (20, 0.05) },
            { "Razorback", (5, 0.10) },
            { "Double Razorback", (20, 0.05) },
            { "Snouter", (10, 0.07) },
            { "Double Snouter", (40, 0.03) },
            { "Leaning Jowler", (15, 0.04) },
            { "Double Leaning Jowler", (60, 0.01) },
            { "Making Bacon", (0, 0.01) }
        };

        public string RollPigs(out int points)
        {
            double roll = _random.NextDouble();
            double cumulative = 0.0;

            foreach (var entry in _pigRollProbabilities)
            {
                cumulative += entry.Value.Probability;
                if (roll <= cumulative)
                {
                    points = entry.Value.Points;
                    return entry.Key;
                }
            }

            points = 0;
            return "Pig Out"; // Default case (should never reach here)
        }

        public void HandlePigRoll(ref PlayerModel player, ref int playerScore, ref bool turnOver, ref bool isMakingBacon)
        {
            string rollResult = RollPigs(out int points);

            if (rollResult == "Pig Out")
            {
                playerScore = 0;
                turnOver = true;
                return;
            }
            if (rollResult == "Making Bacon")
            {
                isMakingBacon = true;
                turnOver = true;
                return;
            }

            playerScore += points;
            if (playerScore >= 100)
            {
                player.HasWon = true;
                turnOver = true;
                return;
            }
        }
    }
}
