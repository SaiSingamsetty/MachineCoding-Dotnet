using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class Player
    {
        public string Name { get; }

        public char Piece { get; }

        public List<Location> Moves { get; }

        public Player(string name, char piece)
        {
            Name = name;
            Piece = piece;
            Moves = new List<Location>();
        }

        public void AddAMove(Location loc)
        {
            Moves.Add(loc);
        }
    }
}