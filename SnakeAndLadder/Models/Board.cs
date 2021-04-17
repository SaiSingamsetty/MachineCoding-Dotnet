using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadder.Models
{
    public class Board
    {
        private readonly int _boardSize;

        public int GetEndOfTheBoard()
        {
            return _boardSize;
        }
        
        public Board(int boardSize)
        {
            _boardSize = boardSize;
        }
    }
}
