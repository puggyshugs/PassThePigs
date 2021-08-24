using System;
using static System.Console;

namespace PassThePigsGame.PlayMethods
{
    public class PlayMethods
    {
        private int Score;
        private Random RandomGenerator;
        public PlayMethods()
        {
            Score = 0;
            RandomGenerator = new Random();
        }

        public void PlayRound()
        {
            // Method that runs one round of rolling and guessing
            Clear();
            System.Console.WriteLine("It's time to PASS these PIGS! (pass)");
            string response = ReadLine().Trim().ToLower();

            PigThrows PigPass = (PigThrows)(RandomGenerator.Next(1, 12));

            if (response == "pass")
            {

                switch (PigPass)
                {
                    case PigThrows.PigOut:
                        Score = 0;
                        System.Console.WriteLine("PIG OUT!!!\nThe Pigs have spoken, you earn nothing for this pitiful attempt...");
                        break;
                    case PigThrows.Sider:
                        Score = Score + 1;
                        System.Console.WriteLine($"Sider, 1 point\nYour score is {Score}");
                        break;
                    case PigThrows.Trotter:
                        Score = Score + 5;
                        System.Console.WriteLine($"Trotter, 5 points!\nYour score is {Score}");
                        break;
                    case PigThrows.DoubleTrotter:
                        Score = Score + 20;
                        System.Console.WriteLine($"Double Trotter, 20 points\nYour score is {Score}");
                        break;
                    case PigThrows.Razorback:
                        Score = Score + 5;
                        System.Console.WriteLine($"Razorback, 5 points\nYour score is {Score}");
                        break;
                    case PigThrows.DoubleRazorback:
                        Score = Score + 20;
                        System.Console.WriteLine($"Double Razorback, 20 points\nYour score is {Score}");
                        break;
                    case PigThrows.Snouter:
                        Score = Score + 10;
                        System.Console.WriteLine($"Snouter, 10 points\nYour score is {Score}");
                        break;
                    case PigThrows.DoubleSnouter:
                        Score = Score + 40;
                        System.Console.WriteLine($"Double Snouter, 40 points\nYour score is {Score}");
                        break;
                    case PigThrows.LeaningJowler:
                        Score = Score + 15;
                        System.Console.WriteLine($"Leaning Jowler, 15 points\nYour score is {Score}");
                        break;
                    case PigThrows.DoubleLeaningJowler:
                        Score = Score + 60;
                        System.Console.WriteLine($"Double Leaning Jowler, 60 points\nYour score is {Score}");
                        break;
                    case PigThrows.MakingBacon:
                        Score = 0;
                        System.Console.WriteLine($"You've been caught making bacon\n\nThis is not the time OR place for such fornication.\n\nThe Pigs are disgusted.\nYou lose.\n\nPress any key to continue");
                        ReadKey();
                        Lose();
                        break;
                }
            }

            else
            {
                System.Console.WriteLine("Invalid input.\nPress any button to continue.\n");
                ReadKey();
                PlayRound();
            }

            if (Score >= 100)
            {
                Win();
            }
            else
            {
                PlayAgainOrBank();
            }

        }

        private void Win()
        {
            // Method that increments the score and lets the player know they won
            Clear();
            System.Console.WriteLine("\n\t=== The Pigs favour you. ===\n\n\t=== Congratulations, Pig. ===\n\n\tPress any key to continue.\n");

        }

        private void Lose()
        {
            // Method that lets the player know they lost
            Clear();

            System.Console.WriteLine("You have dishonoured the pigs.\nTonight, it would be wise to sleep with one eye open...");
        }

        private void PlayAgainOrBank()
        {
            // Ask player if they want to play another round
            Write("-PASS- these pigs again or -BANK- your points? (p/b)");
            string playAgainResponse = ReadLine().Trim().ToLower();

            if (playAgainResponse == "p")
            {
                PlayRound();
            }
        }

    }
}