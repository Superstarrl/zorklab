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

    public class Room
    {
        public string Name { get; }
        public string Description { get; set; }

        public Room(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public override string ToString() => Name;
    }
    class Program
    {

        private static readonly Room[,] Rooms = 
            {
                { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
                {new Room("Forest"), new Room("West of House"), new Room("Behind House") },
                {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
            };

        /*
        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static bool IsDirection(Commands command) => Directions.Contains(command);
        */

        static int horizontal;
        static int vertical;
        
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[vertical, horizontal];
            }
        }

        static void Main(string[] args)
        {
            horizontal = 1;
            vertical = 1;

            InitializeRoomDescriptions();


            Console.WriteLine("Welcome to Zork!");

                Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);

                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }   


                Console.WriteLine("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {
                    

                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;
                    
                    case Commands.LOOK:
                        outputString = CurrentRoom.Description;
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

            //Assert.IsTrue(IsDirection(direction), "Invalid direction.");

            switch (direction)
            {
                case Commands.WEST when horizontal > 0:
                    horizontal--;
                    break;

                case Commands.EAST when horizontal < Rooms.GetLength(0) - 1:
                    horizontal++;
                    break;

                case Commands.SOUTH when vertical < Rooms.GetLength(1) - 1:
                    vertical++;
                    break;

                case Commands.NORTH when vertical > 0:
                    vertical--;
                    break;

                default:
                    return false;

            }

            return true;
        }
        
        private static void InitializeRoomDescriptions()
            {
                var roomMap = new Dictionary<string, Room>();
                foreach (Room room in Rooms)
                {
                    roomMap[room.Name] = room;
                }

                roomMap["Rocky Trail"].Description = "You are on a rock-strewn trail.";
                roomMap["South of House"].Description = "You are facing the south side of a white house. There is no door here, and all the windows are barred.";
                roomMap["Canyon View"].Description = "You are at the top of the Great Canyon on its south wall.";
                roomMap["Forest"].Description = "This is a forest, with trees in all directions around you.";
                roomMap["West of House"].Description = "This is an open field west of a white house, with a boarded front door.";
                roomMap["Behind House"].Description = "You are behind the white house. In one corner of the house there is a small window which is slightly ajar.";
                roomMap["Dense Woods"].Description = "This is a dimly lit forest, with large trees all around. To the east, there appears to be sunlight.";
                roomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here, and all the windows are barred.";
                roomMap["Clearing"].Description = "You are in a clearing, with a forest surrounding you on the west and south.";
            }

    }
}
