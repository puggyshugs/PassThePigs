using System;
using System.Threading;
using PassThePigsGames;

namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {

            PassThePigs myGame = new PassThePigs();
            myGame.Start();

            // Random rand = new Random();
            // int number = rand.Next(1, 5);
            // System.Console.WriteLine(number);

            // int[] numbers = new int[10];
            // for (int i = 0; i < numbers.Length; i++)
            // {
            //     numbers[i] = rand.Next(1, 50);
            // }

            // foreach (var n in numbers)
            // {
            //     System.Console.WriteLine(n + " ");
            // }

            //     int player1 = rand.Next(1, 14);
            //     int player2 = rand.Next(1, 14);

            //     System.Console.WriteLine("Dice1: " + player1);
            //     System.Console.WriteLine("Dice2: " + player2);

            //     if (player1 == 1)
            //     {
            //         System.Console.WriteLine("You are a stinky pig and you have pigged Out! Player 2's go!");

            //     }
            //     else if (player1 == 2)
            //     {
            //         System.Console.WriteLine("Sider, 1 point");

            //     }
            //     else if (player1 == 3)
            //     {
            //         System.Console.WriteLine("Trotter, 5 points!");

            //     }
            //     else if (player1 == 4)
            //     {
            //         System.Console.WriteLine("Double Trotter, 20 points");

            //     }
            //     else if (player1 == 5)
            //     {
            //         System.Console.WriteLine("Razorback, 5 points");

            //     }
            //     else if (player1 == 6)
            //     {
            //         System.Console.WriteLine("Double Razorback, 20 points");

            //     }
            //     else if (player1 == 7)
            //     {
            //         System.Console.WriteLine("Snouter, 10 points");

            //     }
            //     else if (player1 == 8)
            //     {
            //         System.Console.WriteLine("Double Snouter, 40 points");

            //     }
            //     else if (player1 == 9)
            //     {
            //         System.Console.WriteLine("Leaning Jowler, 15 points");

            //     }
            //     else if (player1 == 10)
            //     {
            //         System.Console.WriteLine("Double Leaning Jowler, 60 points");

            //     }
            //     else if (player1 == 11)
            //     {
            //         System.Console.WriteLine("Making Bacon - You've lost all of you points");

            //     }
            //     else if (player1 == 12)
            //     {
            //         System.Console.WriteLine("dice2 wins");

            //     }
            //     else if (player1 == 13)
            //     {
            //         System.Console.WriteLine("dice2 wins");

            //     }

            //     Console.ReadLine();
        }
    }
}
