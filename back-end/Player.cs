using static System.Console;
using System;

namespace PassThePigsGame.PlayerClass
{
    public class Player
    {
        string Name;

        public Player(string name)
        {
            Name = name;
        }

        public void ChoosePlayerAmount()
        {
            string numOfPlayers = ReadLine().Trim().ToLower();

            if (numOfPlayers == "3")
            {
                Console.WriteLine("Player 1, what is your name?");
                string player1Name = ReadLine().Trim();

                Console.WriteLine("Player 2, what is your name?");
                string player2Name = ReadLine().Trim();

                Console.WriteLine("Player 3, what is your name?");
                string player3Name = ReadLine().Trim();

                Player player1 = new Player(player1Name);
                Player player2 = new Player(player2Name);
                Player player3 = new Player(player3Name);
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
            else
            {
                System.Console.WriteLine("Invalid selection.");
                ReadKey();
                ChoosePlayerAmount();
            }
        }

    }

}