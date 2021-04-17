using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadder.Models
{
    public class Board
    {
        public int BoardSize { get; set; }

        public Board(int boardSize)
        {
            BoardSize = boardSize;
        }
    }
}
