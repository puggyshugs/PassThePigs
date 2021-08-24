using static System.Console;

namespace PassThePigsGame.Program
{
    public class Program
    {
        static void Main(string[] args)
        {
            Clear();
            System.Console.WriteLine("How many little piggies want to play?");
            System.Console.WriteLine("\n\t 3, 4, 5 or 6?");

            Game.Game myGame = new Game.Game("Pass The Pigs");
            myGame.Start();
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
