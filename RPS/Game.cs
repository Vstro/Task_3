using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace RPS
{
    class Game
    {
        private List<String> availableMoves = null;

        public Game(String[] availableMoves)
        {
            this.availableMoves = availableMoves.ToList<String>();
        }

        public String GenerateComputerMove(out String plainMove, out String key)
        {
            byte[] keyBytes = new byte[32];
            RandomNumberGenerator.Create().GetBytes(keyBytes);
            key = BytesToString(keyBytes);
            plainMove = availableMoves[new Random().Next() % availableMoves.Count];
            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes))
            {
                hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(plainMove));
                return BytesToString(hmacsha256.Hash);
            }
        }

        public GameResult GetFirstPlayerResult(String firstMove, String secondMove)
        {
            if (firstMove.Equals(secondMove))
            {
                return GameResult.Tie;
            }
            int distanceFromFirstClockwise = availableMoves.IndexOf(secondMove) - availableMoves.IndexOf(firstMove);
            if (distanceFromFirstClockwise < 0)
            {
                distanceFromFirstClockwise += availableMoves.Count;
            }
            if (distanceFromFirstClockwise > ((availableMoves.Count - 1) / 2))
            {
                return GameResult.Win;
            }
            return GameResult.Lose;
        }

        public static bool AreProperMoves(String[] moves)
        {
            if ((moves.Length < 3) || (moves.Length % 2 == 0))
            {
                return false;
            }
            if (new HashSet<String>(moves).Count < moves.Length)
            {
                return false;
            }
            return true;
        }

        private static String BytesToString(byte[] bytes)
        {
            String hexRepresentation = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexRepresentation += bytes[i].ToString("X2");
            }               
            return hexRepresentation;
        }
    }
}
