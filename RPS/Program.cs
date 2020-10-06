using System;

namespace RPS
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] moves = args;
            if (!Game.AreProperMoves(moves))
            {
                Console.WriteLine("Error: Incorrect arguments!");
                return;
            }
            Game game = new Game(moves);
            String computerMove;
            String key;
            Console.WriteLine($"HMAC: {game.GenerateComputerMove(out computerMove, out key)}");
            uint playerMove;
            do
            {
                printMenu(moves);
            } while (!uint.TryParse(Console.ReadLine(), out playerMove) || playerMove > moves.Length);
            if (playerMove == 0)
            {
                Console.WriteLine("Goodbye...");
                return;
            }
            Console.WriteLine($"Your move: {moves[playerMove - 1]}");
            Console.WriteLine("Computer move: " + computerMove);
            GameResult result = game.GetFirstPlayerResult(moves[playerMove - 1], computerMove);
            if (result == GameResult.Win)
            {
                Console.WriteLine("You win!");
            } else if (result == GameResult.Lose)
            {
                Console.WriteLine("You lose!");
            } else
            {
                Console.WriteLine("You tie!");
            }
            Console.WriteLine("HMAC key: " + key);
        }

        private static void printMenu(String[] moves)
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }
            Console.WriteLine("0 - exit");
            Console.Write("Enter your move: ");
        }
    }
}
