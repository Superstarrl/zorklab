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
        private static readonly string[] Rooms = 
            {
                "Forest", "West of House", "Behind House", "Clearing", "Canyon View"
            };

        static int location;

        static void Main(string[] args)
        {
            location = 1;


            Console.WriteLine("Welcome to Zork!");
            

            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(Rooms[location]);
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

                    //case Commands.NORTH:
                    //case Commands.SOUTH:
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
                if (location == 0) return false;
                location--;
                return true;
            }

            if (direction == Commands.EAST)
            {
                if (location == Rooms.Length - 1) return false;
                location++;
                return true;
            }

            return false;
        }
    }
}
