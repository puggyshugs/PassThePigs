using System;
using PassThePigsGame.PlayerClass;
using static System.Console;
namespace PassThePigsGame.Game
{
    public class Game
    {
        private string _gameName;
        PlayMethods.PlayMethods _playMethods = new PlayMethods.PlayMethods();

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
                ChoosePlayerAmount();
                ReadKey();
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

        public void ChoosePlayerAmount()
        {

            System.Console.WriteLine("How many lil piggies are there?");
            string numOfPlayers = ReadLine().Trim().ToLower();

            if (numOfPlayers == "3")
            {
                Clear();
                Console.WriteLine("Player 1, what is your name?");
                string player1Name = ReadLine().Trim();

                Clear();
                Console.WriteLine("Player 2, what is your name?");
                string player2Name = ReadLine().Trim();

                Clear();
                Console.WriteLine("Player 3, what is your name?");
                string player3Name = ReadLine().Trim();

                Player player1 = new Player(player1Name);
                Player player2 = new Player(player2Name);
                Player player3 = new Player(player3Name);

                _playMethods.PlayRound();
            }
            else if (numOfPlayers == "4")
            {
                Console.WriteLine("Player 1, what is your name?");
                string player1Name = ReadLine().Trim();

                Console.WriteLine("Player 2, what is your name?");
                string player2Name = ReadLine().Trim();

                Console.WriteLine("Player 3, what is your name?");
                string player3Name = ReadLine().Trim();

                Console.WriteLine("Player 4, what is your name?");
                string player4Name = ReadLine().Trim();

                Player player1 = new Player(player1Name);
                Player player2 = new Player(player2Name);
                Player player3 = new Player(player3Name);
                Player player4 = new Player(player4Name);
            }
            else if (numOfPlayers == "5")
            {
                Console.WriteLine("Player 1, what is your name?");
                string player1Name = ReadLine().Trim();

                Console.WriteLine("Player 2, what is your name?");
                string player2Name = ReadLine().Trim();

                Console.WriteLine("Player 3, what is your name?");
                string player3Name = ReadLine().Trim();

                Console.WriteLine("Player 4, what is your name?");
                string player4Name = ReadLine().Trim();

                Console.WriteLine("Player 5, what is your name?");
                string player5Name = ReadLine().Trim();

                Player player1 = new Player(player1Name);
                Player player2 = new Player(player2Name);
                Player player3 = new Player(player3Name);
                Player player4 = new Player(player4Name);
                Player player5 = new Player(player5Name);
            }
            else if (numOfPlayers == "6")
            {
                Console.WriteLine("Player 1, what is your name?");
                string player1Name = ReadLine().Trim();

                Console.WriteLine("Player 2, what is your name?");
                string player2Name = ReadLine().Trim();

                Console.WriteLine("Player 3, what is your name?");
                string player3Name = ReadLine().Trim();

                Console.WriteLine("Player 4, what is your name?");
                string player4Name = ReadLine().Trim();

                Console.WriteLine("Player 5, what is your name?");
                string player5Name = ReadLine().Trim();

                Console.WriteLine("Player 6, what is your name?");
                string player6Name = ReadLine().Trim();

                Player player1 = new Player(player1Name);
                Player player2 = new Player(player2Name);
                Player player3 = new Player(player3Name);
                Player player4 = new Player(player4Name);
                Player player5 = new Player(player5Name);
                Player player6 = new Player(player6Name);
            }
            else
            {
                System.Console.WriteLine("Invalid selection.");
                ReadKey();
                ChoosePlayerAmount();
            }
        }
    }
}