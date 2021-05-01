using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace PassThePigsGame.PassThePigsClass
{
    public class PassThePigs
    {
        private int Score;
        private string GameName;
        private Random RandomGenerator;

        public enum PigThrows
        {
            PigOut,
            Sider,
            Trotter,
            DoubleTrotter,
            Razorback,
            DoubleRazorback,
            Snouter,
            DoubleSnouter,
            LeaningJowler,
            DoubleLeaningJowler,
            MakingBacon

        }

        public PassThePigs()
        {
            //initialise anything we need later on:
            Score = 0;
            GameName = "Pass The Pigs";
            RandomGenerator = new Random();
        }

        public void Start()
        {
            //Method that starts the game.
            Clear();
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
                Clear();
                System.Console.WriteLine("\n  === The Pigs will sing to your honour! ===  ");
                System.Console.WriteLine("\n\t* Press any key to continue *  ");
                ReadKey();
                PlayRound();

            }
            else
            {
                Clear();
                System.Console.WriteLine("\nThe Pigs are displeased. They will not forget this desertion.");

            }
            System.Console.WriteLine("\nPress any key to exit");
        }
        private void PlayRound()
        {
            // Method that runs one round of rolling and guessing
            Clear();
            System.Console.WriteLine("It's time to PASS these PIGS! (pass)");
            string response = ReadLine().Trim().ToLower();

            PigThrows PigPass = (PigThrows)(RandomGenerator.Next(1, 12));

            switch (PigPass)
            {
                case PigThrows.PigOut:
                    Score = 0;
                    System.Console.WriteLine("PIG OUT!!!");
                    System.Console.WriteLine("The Pigs have spoken, you earn nothing for this pitiful attempt...");
                    break;
                case PigThrows.Sider:
                    Score = Score + 1;
                    System.Console.WriteLine("Sider, 1 point");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.Trotter:
                    Score = Score + 5;
                    System.Console.WriteLine("Trotter, 5 points!");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.DoubleTrotter:
                    Score = Score + 20;
                    System.Console.WriteLine("Double Trotter, 20 points");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.Razorback:
                    Score = Score + 5;
                    System.Console.WriteLine("Razorback, 5 points");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.DoubleRazorback:
                    Score = Score + 20;
                    System.Console.WriteLine("Double Razorback, 20 points");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.Snouter:
                    Score = Score + 10;
                    System.Console.WriteLine("Snouter, 10 points");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.DoubleSnouter:
                    Score = Score + 40;
                    System.Console.WriteLine("Double Snouter, 40 points");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.LeaningJowler:
                    Score = Score + 15;
                    System.Console.WriteLine("Leaning Jowler, 15 points");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.DoubleLeaningJowler:
                    Score = Score + 60;
                    System.Console.WriteLine("Double Leaning Jowler, 60 points");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
                case PigThrows.MakingBacon:
                    Score = 0;
                    System.Console.WriteLine("You've been caught making bacon");
                    System.Console.WriteLine("\nThis is not the time OR place for such fornication.");
                    System.Console.WriteLine("\nThe Pigs are disgusted.");
                    System.Console.WriteLine($"Your score is {Score}");
                    break;
            }

            if (Score >= 100)
            {
                Win();
            }
            else
            {
                // System.Console.WriteLine("\n\t* Press any key to continue *  ");
                // ReadKey();
                PlayAgainOrBank();
            }

            PlayAgainOrBank();
        }

        private void Win()
        {
            // Method that increments the score and lets the player know they won
            System.Console.WriteLine("\n\t=== The Pigs favour you. ===");
            System.Console.WriteLine("\n\t=== Congratulations, Pig. ===");
            System.Console.WriteLine("\nPress any key to continue.");

        }

        private void Lose()
        {
            // Method that lets the player know they lost
        }

        private void PlayAgainOrBank()
        {
            // Ask player if they want to play another round
            Write("Pass these pigs again or bank your points? (pass/bank)");
            string playAgainResponse = ReadLine().Trim().ToLower();

            if (playAgainResponse == "pass")
            {
                PlayRound();
            }
        }
    }
}