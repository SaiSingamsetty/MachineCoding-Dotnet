using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeAndLadder.Models
{
    public class Dice
    {
        private readonly int _count;

        public Dice(int noOfDices)
        {
            _count = noOfDices;
        }

        public int Roll()
        {
            var rand = new Random().Next(_count, _count * 3 + 1);
            return rand;
        }
    }
}
