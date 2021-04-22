using System;
using System.Threading;

namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
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

            int dice1 = rand.Next(1, 7);
            int dice2 = rand.Next(1, 7);

            System.Console.WriteLine("Dice1: " + dice1);
            System.Console.WriteLine("Dice2: " + dice2);

            if (dice1 > dice2)
            {
                System.Console.WriteLine("dice1 wins");

            }
            else if (dice2 > dice1)
            {
                System.Console.WriteLine("dice2 wins");

            }
            else
            {
                System.Console.WriteLine("draw");
            }

            Console.ReadLine();
        }
    }
}
