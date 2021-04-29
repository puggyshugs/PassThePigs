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
            System.Console.WriteLine($"=== {GameName} ===");
            System.Console.WriteLine("\nLet's Pass these Pigs!");
            System.Console.WriteLine("\nInstructions:");
            System.Console.WriteLine("\t> You will roll the pigs, each landing position will mean different points!");
            System.Console.WriteLine("\t> You can bank you points at any time and add them to your final score.");
            System.Console.WriteLine("\t> But be warned! Rolling a Pig Out will result in 0 points for that roll.");
            System.Console.WriteLine("\nHave fun! Ready to play? (yes/no)");

            string playResponse = ReadLine();
            if (playResponse == "yes")
            {
                System.Console.WriteLine("The Pigs will sing to your honour!");
            }
            else
            {
                System.Console.WriteLine("The Pigs are displeased. They will not forget this desertion.");
            }

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