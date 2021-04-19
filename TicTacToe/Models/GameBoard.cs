using System;
using System.Text;

namespace TicTacToe.Models
{
    public class GameBoard
    {
        public char[,] Board { get; }

        private readonly int _size;

        public void DisplayBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Console.Write(Board[i, j] + " ");
                }
                Console.WriteLine("\t");
            }
        }

        public void WriteToBoard(Location loc, char c)
        {
            Board[loc.X, loc.Y] = c;
        }

        public char Get(Location loc)
        {
            return Board[loc.X, loc.Y];
        }

        public GameBoard(int size)
        {
            _size = size;
            Board = new char[size, size];

            //Make each char '-'
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Board[i, j] = '-';
                }
            }
        }
    }
}
