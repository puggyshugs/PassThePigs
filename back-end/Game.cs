using System;
using static System.Console;
using PassThePigsGame.PlayMethods;

namespace PassThePigsGame.Game
{
    public class Game
    {
        private string _gameName;

        public Game(string gameName)
        {
            _gameName = gameName;
        }
        public void Start()
        {
            //Method that starts the game.
            Clear();
            Title = _gameName;
            System.Console.WriteLine($"=== {_gameName} ===");
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
            else if (playResponse == "no")
            {
                Clear();
                System.Console.WriteLine("\nThe Pigs are displeased. They will not forget this desertion.");

            }
            else
            {
                System.Console.WriteLine("Invalid response, press any key to continue.");
                ReadKey();
                Start();
            }

            System.Console.WriteLine("\nPress any key to exit");
        }

    }
}