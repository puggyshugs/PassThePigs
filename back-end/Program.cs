using System;
using System.Threading;
using PassThePigsGame.Game;

namespace PassThePigsGame.Program
{
    public class Program
    {
        static void Main(string[] args)
        {

            Game.Game myGame = new Game.Game();
            myGame.Start();

            // int xy = Add(4, 5);
            // System.Console.WriteLine(xy);
            // System.Console.WriteLine(IsOdd(5));

        }

        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}
