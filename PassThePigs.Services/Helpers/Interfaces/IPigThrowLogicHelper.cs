using System;
using PassThePigs.Domain;

namespace PassThePigs.Services.Helpers.Interfaces;

public interface IPigThrowLogicHelper
{
    string RollPigs(out int points);
    void HandlePigRoll(ref PlayerModel player, ref int playerScore, ref bool turnOver, ref bool isMakingBacon);
}
