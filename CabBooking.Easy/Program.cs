using System;
using CabBooking.Easy.Commands;
using CabBooking.Easy.Models;
using CabBooking.Easy.Services;

namespace CabBooking.Easy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("! Parking Lot Service !");

            var lotService = new ParkingLotService();
            var commandFactory = new CommandExecutorFactory(lotService);

            while (true)
            {
                try
                {
                    var stringInput = Console.ReadLine();
                    var command = new Command(stringInput);

                    var executor = commandFactory.GetCommandExecutor(command);
                    var isValidCommand = executor.Validate(command);
                    if (!isValidCommand)
                    {
                        throw new Exception("Invalid Command");
                    }

                    executor.Execute(command);
                    if (command.GetCommandName().Equals("exit"))
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
        }
    }
}
