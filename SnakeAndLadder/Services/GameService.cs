﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using SnakeAndLadder.Models;

namespace SnakeAndLadder.Services
{
    public class GameService
    {
        private List<Snake> Snakes { get; set; }

        private List<Ladder> Ladders { get; set; }

        private Queue<Player> Players { get; set; }

        private Board Board { get; set; }

        private Dice Dice { get; set; }

        public GameService(List<List<int>> snakeIndices, List<List<int>> ladderIndices, List<string> playersData, int boardSize = 100, int diceSize = 1)
        {
            Dice = new Dice(diceSize);
            Board = new Board(boardSize);
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
                var currentPlayer = Players.Peek();
                var diceValue = Dice.Roll();
                var newPosition = currentPlayer.CurrentPosition + diceValue;

                if (newPosition > Board.BoardSize)
                {
                    //Set the player in same position
                    //Move to next player
                    Players.Enqueue(Players.Dequeue());
                }
                else
                {
                    var oldPosition = currentPlayer.CurrentPosition;
                    currentPlayer.CurrentPosition = GetNextPosition(newPosition);
                    if (currentPlayer.CurrentPosition == Board.BoardSize)
                    {
                        currentPlayer.DidComplete = true;
                        Console.WriteLine($"{currentPlayer.Name} wins the game");
                    }
                    else
                    {
                        Console.WriteLine($"{currentPlayer.Name} rolled a {diceValue} and moved from {oldPosition} to {currentPlayer.CurrentPosition}");
                        Players.Enqueue(Players.Dequeue());
                    }
                }

                if (Players.Any(x => x.DidComplete)) //Players.Size() < 2  : To make game available till 2 players left
                {
                    break;
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
                if(Ladders.Any(x=>x.Start == ladderIndex[0]))
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
                Players.Enqueue(new Player()
                {
                    CurrentPosition = 0,
                    DidComplete = false,
                    Name = eachPlayer
                });
            }
        }

        private int GetNextPosition(int newPosition)
        {
            // Checking snakes
            foreach (var snake in Snakes)
            {
                if (snake.Head == newPosition)
                    return snake.Tail;
            }

            // Checking ladders
            foreach (var ladder in Ladders)
            {
                if (ladder.Start == newPosition)
                    return ladder.End;
            }

            return newPosition;
        }
    }
}
