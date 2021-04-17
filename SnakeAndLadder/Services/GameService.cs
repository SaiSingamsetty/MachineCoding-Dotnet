using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using SnakeAndLadder.Exceptions;
using SnakeAndLadder.Models;

namespace SnakeAndLadder.Services
{
    public class GameService
    {
        private List<Snake> Snakes { get; }

        private List<Ladder> Ladders { get; }

        private Queue<Player> Players { get; }

        private Board Board { get; }

        private Dice Dice { get; }

        public GameService(List<List<int>> snakeIndices, List<List<int>> ladderIndices, List<string> playersData, int boardSize = 100, int diceSize = 1)
        {
            Dice = new Dice(diceSize);
            Board = new Board(boardSize);
            Snakes = new List<Snake>();
            Ladders = new List<Ladder>();
            Players = new Queue<Player>();

            SetUpTheBoard(snakeIndices, ladderIndices, playersData);
        }

        private void SetUpTheBoard(List<List<int>> snakeIndices, List<List<int>> ladderIndices, List<string> playersData)
        {
            SetSnakesOnGame(snakeIndices);
            SetLaddersOnGame(ladderIndices);
            SetPlayers(playersData);
        }

        public void StartGame()
        {
            while (true)
            {
                try
                {
                    var currentPlayer = Players.Peek();
                    var diceValue = Dice.Roll();
                    var newPosition = currentPlayer.GetPlayersCurrentPosition() + diceValue;

                    if (newPosition > Board.GetEndOfTheBoard())
                    {
                        //Set the player in same position
                        //Move to next player
                        Players.Enqueue(Players.Dequeue());
                    }
                    else
                    {
                        var oldPosition = currentPlayer.GetPlayersCurrentPosition();
                        currentPlayer.SetPositionForPlayer(GetNextPosition(newPosition));
                        if (currentPlayer.GetPlayersCurrentPosition() == Board.GetEndOfTheBoard())
                        {
                            currentPlayer.SetWinningFlag();
                            Console.WriteLine($"{currentPlayer.GetPlayerName()} rolled a {diceValue} and moved from {oldPosition} to {currentPlayer.GetPlayersCurrentPosition()}");
                            Console.Write($", {currentPlayer.GetPlayerName()} wins the game");
                        }
                        else
                        {
                            Console.WriteLine($"{currentPlayer.GetPlayerName()} rolled a {diceValue} and moved from {oldPosition} to {currentPlayer.GetPlayersCurrentPosition()}");
                            Players.Enqueue(Players.Dequeue());
                        }
                    }

                    if (Players.Any(x => x.DidPlayerCompletedGame())) //Players.Size() < 2  : To make game available till 2 players left
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    throw new SnakeAndLadderException(500, e.Message);
                }
            }
        }

        private void SetSnakesOnGame(List<List<int>> snakeIndices)
        {
            foreach (var snakeIndex in snakeIndices)
            {
                if (Snakes.Any(x => x.Head == snakeIndex[0]))
                {
                    //Skipping two snake heads at same point
                    continue;
                }

                var snake = new Snake()
                {
                    Head = snakeIndex[0],
                    Tail = snakeIndex[1]
                };
                Snakes.Add(snake);
            }
        }

        private void SetLaddersOnGame(List<List<int>> ladderIndices)
        {
            foreach (var ladderIndex in ladderIndices)
            {
                if (Ladders.Any(x => x.Start == ladderIndex[0]))
                {
                    //Skipping two Ladder starts at same point
                    continue;
                }

                Ladders.Add(new Ladder()
                {
                    Start = ladderIndex[0],
                    End = ladderIndex[1]
                });
            }
        }

        private void SetPlayers(List<string> playersData)
        {
            foreach (var eachPlayer in playersData)
            {
                Players.Enqueue(new Player(eachPlayer));
            }
        }

        private int GetNextPosition(int newPosition)
        {
            // Checking snakes
            foreach (var snake in Snakes)
            {
                if (snake.Head == newPosition)
                {
                    Console.WriteLine($"Snake Bite: {newPosition} to {snake.Tail}");
                    return snake.Tail;
                }
            }

            // Checking ladders
            foreach (var ladder in Ladders)
            {
                if (ladder.Start == newPosition)
                {
                    Console.WriteLine($"WooHoo ! Ladder found {newPosition} to {ladder.End}");
                    return ladder.End;
                }
            }

            return newPosition;
        }
    }
}
