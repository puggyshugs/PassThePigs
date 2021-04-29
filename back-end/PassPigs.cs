using System;
using System.Collections.Generic;
using System.Text;


using static System.Console;
namespace PassThePigsGames
{
    class PassPigs
    {
        private int Score;
        private string GameName;
        private Random RandomGenerator;

        public PassPigs()
        {
            //initialise anything we need later on:
            Score = 0;
            GameName = "Pass The Pigs";
            RandomGenerator = new Random();
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

            string playResponse = ReadLine().Trim().ToLower();
            if (playResponse == "yes")
            {
                System.Console.WriteLine("\n  === The Pigs will sing to your honour! ===  ");
                PlayRound();

            }
            else
            {
                System.Console.WriteLine("\nThe Pigs are displeased. They will not forget this desertion.");

            }
            System.Console.WriteLine("\nPress any key to exit");
        }
        private void PlayRound()
        {
            // Method that runs one round of rolling and guessing
            Clear();
            System.Console.WriteLine("Pass these Pigs or bank your points? (pass/bank)");
            string response = ReadLine().Trim().ToLower();
            System.Console.WriteLine($"You want to {response}.");

            int PigPass = RandomGenerator.Next(1, 12);
            if (PigPass == 1)
            {
                System.Console.WriteLine("You are a stinky pig and you have pigged out! Player 2's go!");

            }
            else if (PigPass == 2)
            {
                System.Console.WriteLine("Sider, 1 point");

            }
            else if (PigPass == 3)
            {
                System.Console.WriteLine("Trotter, 5 points!");

            }
            else if (PigPass == 4)
            {
                System.Console.WriteLine("Double Trotter, 20 points");

            }
            else if (PigPass == 5)
            {
                System.Console.WriteLine("Razorback, 5 points");

            }
            else if (PigPass == 6)
            {
                System.Console.WriteLine("Double Razorback, 20 points");

            }
            else if (PigPass == 7)
            {
                System.Console.WriteLine("Snouter, 10 points");

            }
            else if (PigPass == 8)
            {
                System.Console.WriteLine("Double Snouter, 40 points");

            }
            else if (PigPass == 9)
            {
                System.Console.WriteLine("Leaning Jowler, 15 points");

            }
            else if (PigPass == 10)
            {
                System.Console.WriteLine("Double Leaning Jowler, 60 points");

            }
            else if (PigPass == 11)
            {
                System.Console.WriteLine("Making Bacon - You've lost all of you points");

            }
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