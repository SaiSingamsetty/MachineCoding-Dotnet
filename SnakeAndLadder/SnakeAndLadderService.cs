using SnakeAndLadder.Exceptions;
using SnakeAndLadder.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeAndLadder
{
    public class SnakeAndLadderService
    {
        public SnakeAndLadderService()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            //Set up Snakes
            var noOfSnakes = Console.ReadLine().ToInteger();

            var listOfSnakeIndices = new List<List<int>>();
            for (int i = 0; i < noOfSnakes; i++)
            {
                var snakeHeadTail = Console.ReadLine()?.Split(' ').Select(x => x.ToInteger())?.ToList();
                if (snakeHeadTail == null)
                {
                    Console.WriteLine("Please enter some valid values");
                    i--;
                    continue;
                }

                if (snakeHeadTail[0] <= snakeHeadTail[1])
                {
                    throw new SnakeAndLadderException(400, "Snake head position should be greater than tail position");
                }

                listOfSnakeIndices.Add(snakeHeadTail);
            }

            var noOfLadders = Console.ReadLine().ToInteger();
            var listOfLadderIndices = new List<List<int>>();
            for (int i = 0; i < noOfLadders; i++)
            {
                var ladderStartEnd = Console.ReadLine()?.Split(' ').Select(x => x.ToInteger())?.ToList();
                if (ladderStartEnd == null)
                {
                    Console.WriteLine("Please enter some valid values");
                    i--;
                    continue;
                }
                if (ladderStartEnd[0] >= ladderStartEnd[1])
                {
                    throw new SnakeAndLadderException(400, "Ladder start position should always be less than end position");
                }

                listOfLadderIndices.Add(ladderStartEnd);
            }

            var noOfPlayers = Console.ReadLine().ToInteger();
            var listOfPlayerNames = new List<string>();
            for (int i = 0; i < noOfPlayers; i++)
            {
                listOfPlayerNames.Add(Console.ReadLine());
            }

            var game = new GameService(listOfSnakeIndices, listOfLadderIndices, listOfPlayerNames, 30);
            game.StartGame();
        }
    }

    public static class Helpers
    {
        public static int ToInteger(this string str)
        {
            return Convert.ToInt32(str);
        }
    }
}
