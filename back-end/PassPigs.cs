using System;
using System.Collections.Generic;
using System.Text;


using static System.Console;
namespace PassThePigsGames
{
    class PassPigs
    {
        private string GameName;
        private int Score;

        public PassPigs()
        {
            //initialise anything we need later on:
            Score = 0;
            GameName = "Pass The Pigs";
        }

        public void Start()
        {
            //Method that starts the game.

            Title = GameName;
            System.Console.WriteLine($"==={GameName}===");
            System.Console.WriteLine("Press any key to exit");
            ReadKey();
        }

        private void PlayRound()
        {
            // Method that runs one round of rolling and guessing
        }

        private void Win()
        {
            // Method that increments the score and lets the player know they won
        }

        private void Lose()
        {
            // Method that lets the player know they lost
        }

        private void AskToPlayAgain()
        {
            // Ask player if they want to play another round
        }
    }
}