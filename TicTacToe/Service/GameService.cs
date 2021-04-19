using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Models;

namespace TicTacToe.Service
{
    public class GameService
    {
        public GameService(int size = GameSize)
        {
            _board = new GameBoard(size);
            _players = new Queue<Player>();
        }

        private const int GameSize = 3;

        private readonly GameBoard _board;

        private readonly Queue<Player> _players;

        private int _emptyBoxes = GameSize * GameSize;

        public GameMoveStatus MakeAMove(Location location)
        {
            var player = _players.Peek();
            var existingValue = _board.Get(location);

            if (!Piece.ValidateMove(existingValue)) 
                return GameMoveStatus.InvalidMove;

            //Write new piece to Board
            _board.WriteToBoard(location, player.Piece);
                
            //Dequeue and enqueue the player
            _players.Enqueue(_players.Dequeue());

            //Decrease emptyBoxes count
            _emptyBoxes--;

            //Add move to player moves
            player.AddAMove(location);

            //Check if this is winning move
            if (IsWinningMove(player.Piece, location))
            {
                Console.WriteLine($"{player.Name} won the game");
                return GameMoveStatus.WinningMove;
            }

            //It can be Game completion move or a Valid move
            return _emptyBoxes == 0 ? GameMoveStatus.GameMove : GameMoveStatus.ValidMove;
        }

        public bool IsWinningMove(char playerPiece, Location newLocation)
        {
            var isItOnDiagonal = IsNewPieceOnDiagonal(newLocation);

            var rowScore = 0;
            var colScore = 0;

            if (isItOnDiagonal)
            {
                var diagScore = 0;
                var revDiagScore = 0;

                for (int i = 0; i < GameSize; i++)
                {
                    if (_board.Board[newLocation.X, i] == playerPiece)
                    {
                        rowScore++;
                    }

                    if (_board.Board[i, newLocation.Y] == playerPiece)
                    {
                        colScore++;
                    }

                    if (_board.Board[i, i] == playerPiece)
                    {
                        diagScore++;
                    }

                    if (_board.Board[i, GameSize - i - 1] == playerPiece)
                    {
                        revDiagScore++;
                    }
                }

                return new[] {rowScore, colScore, diagScore, revDiagScore}.Any(x => x == GameSize);
            }

            for (int i = 0; i < GameSize; i++)
            {
                if (_board.Board[newLocation.X, i] == playerPiece)
                {
                    rowScore++;
                }

                if (_board.Board[i, newLocation.Y] == playerPiece)
                {
                    colScore++;
                }
            }

            return new[] { rowScore, colScore }.Any(x => x == GameSize);

        }

        private static bool IsNewPieceOnDiagonal(Location loc)
        {
            if (loc.X == loc.Y)
                return true;

            if (loc.X + loc.Y == GameSize - 1)
                return true;
            
            return false;
        }

        public void AddPlayer(Player player)
        {
            _players.Enqueue(player);
        }

        public void DisplayGameBoard()
        {
            _board.DisplayBoard();
        }
    }
}
