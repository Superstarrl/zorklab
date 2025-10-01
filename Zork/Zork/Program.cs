using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{
    enum Commands
    {
        QUIT,
        LOOK,
        NORTH,
        SOUTH,
        EAST,
        WEST,
        UNKNOWN
    }

    

    class Program
    {
        private static readonly string[,] Rooms = 
            {
                { "Rocky Trail", "South of House", "Canyon View"},
                {"Forest", "West of House", "Behind House" },
                {"Dense Woods", "North of House", "Clearing" }
            };

        static int horizontal;
        static int vertical;

        static void Main(string[] args)
        {
            horizontal = 1;
            vertical = 1;


            Console.WriteLine("Welcome to Zork!");
            

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[vertical, horizontal]);
                Console.WriteLine("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    

                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;
                    
                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        if (Move(command))
                            outputString = $"You moved {command}.";
                        else
                            outputString = "The way is shut!";
                        break;

                            default:
                        outputString = "Unknown command.";
                        break;
                }


                Console.WriteLine(outputString);
            }
            

            
        }


        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN; 
        }

        private static bool Move(Commands direction)
        {
            if (direction == Commands.WEST)
            {
                if (horizontal == 0) return false;
                horizontal--;
                return true;
            }

            if (direction == Commands.EAST)
            {
                if (horizontal == Rooms.GetLength(0) - 1) return false;
                horizontal++;
                return true;
            }

            if (direction == Commands.SOUTH)
            {
                if (vertical == Rooms.GetLength(1) - 1) return false;
                vertical++;
                return true;
            }

            if (direction == Commands.NORTH)
            {
                if (vertical == 0) return false;
                vertical--;
                return true;
            }

            return false;
        }
    }
}
