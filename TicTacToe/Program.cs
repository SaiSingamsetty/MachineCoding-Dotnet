using System;
using TicTacToe.Models;
using TicTacToe.Service;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var player1 = Console.ReadLine()?.Split(" ") ?? new string[] { };
            var player2 = Console.ReadLine()?.Split(" ") ?? new string[] { };

            var game = new GameService();
            game.AddPlayer(new Player(player1[1], player1[0][0]));
            game.AddPlayer(new Player(player2[1], player2[0][0]));
            game.DisplayGameBoard();

            while (true)
            {
                try
                {
                    var cmd = Console.ReadLine();
                    if(string.IsNullOrEmpty(cmd) || cmd == "exit")
                        break;

                    var location = cmd.Split(" ");
                    var status = game.MakeAMove(new Location(int.Parse(location[0]) - 1, int.Parse(location[1]) - 1));

                    game.DisplayGameBoard();

                    if (status == GameMoveStatus.GameMove)
                    {
                        Console.WriteLine("\nGame Over");
                        break;
                    }

                    if (status == GameMoveStatus.WinningMove)
                    {
                        break;
                    }

                    if (status == GameMoveStatus.InvalidMove)
                    {
                        Console.WriteLine("Invalid Move");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n"+e.Message);
                }
            }

            Console.WriteLine("\nPress any key to exit");
            Console.ReadKey();
        }
    }
}
