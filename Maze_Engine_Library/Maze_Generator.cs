using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Engine_Library
{
    class Maze_Generator
    {
        /// <summary>
        /// The size of the Map Array.
        /// The Array is the number of rooms * 2 +1.
        /// This is so the map accomodates each room and the walls in between;
        /// </summary>
        public int Size { get; private set; }
        /// <summary>
        /// An Array of type Room, stores each room of the maze and their properties.
        /// </summary>
        public Room[,] Map { get; private set; }
        /// <summary>
        /// The stack the keeps track through the recursive backtracking algorithm.
        /// Also helps escape the recursion.
        /// </summary>
        Stack<Tuple<int, int>> Stack { get; set; }
        /// <summary>
        /// Property of type Random for use when generating random attributes of the maze
        /// </summary>
        Random Random { get; set; }

        /// <summary>
        /// Initializes the Maze Generator and executes the recursive backtracking algorithm
        /// Parameter is the size of the maze to be generated.
        /// </summary>
        /// <param name="size"></param>
        public Maze_Generator(int size)
        {
            Size = (size * 2) + 1;
            Map = new Room[Size, Size];
            Stack = new Stack<Tuple<int, int>>();
            Random = new Random();

            Populate();
            Stack.Push(new Tuple<int, int>(1, 1));
            Carve(1, 1);
            SetExit();
            SetNighbours();
        }

        /// <summary>
        /// Populates the maze rooms.
        /// Creates a grid of Chambers, and fills the rest with walls.
        /// </summary>
        void Populate()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Map[x, y] = new Room(x, y, (x % 2 != 0 && y % 2 != 0) ? RoomTypes.Chamber : RoomTypes.Wall);
                }
            }
        }

        /// <summary>
        /// The recursive method of the backtracking algorithm.
        /// The parameters are the maze coordinates for the next room to calculate.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void Carve(int x, int y)
        {
            bool[] visitedNeighbours = new bool[4]
            {
                (x - 2 >= 0 && x - 2 < Size) ? Map[x-2,y].Visited : true,
                (y + 2 >= 0 && y + 2 < Size) ? Map[x,y+2].Visited : true,
                (x + 2 >= 0 && x + 2 < Size) ? Map[x + 2, y].Visited : true,
                (y - 2 >= 0 && y - 2 < Size) ? Map[x, y - 2].Visited : true
            };

            if (!visitedNeighbours[0] || !visitedNeighbours[1] || !visitedNeighbours[2] || !visitedNeighbours[3])
            {
                int nx = x, ny = y;
                int px = x, py = y;

                List<int> i = new List<int>();
                for (int j = 0; j < 4; j++)
                {
                    if (!visitedNeighbours[j]) i.Add(j);
                }

                switch (i[Random.Next(0, i.Count)])
                {
                    case 0:
                        nx -= 2;
                        px -= 1;
                        break;
                    case 1:
                        ny += 2;
                        py += 1;
                        break;
                    case 2:
                        nx += 2;
                        px += 1;
                        break;
                    case 3:
                        ny -= 2;
                        py -= 1;
                        break;
                }

                Map[px, py].RoomType = RoomTypes.Passage;
                Stack.Push(new Tuple<int, int>(nx, ny));
                Map[nx, ny].Visited = true;
                Carve(nx, ny);
            }
            else if (Stack.Count > 0)
            {
                Tuple<int, int> location = Stack.Pop();
                Carve(location.Item1, location.Item2);
            }
        }

        /// <summary>
        /// Randomly selects a location around the edge of the maze as the exit.
        /// </summary>
        void SetExit()
        {
            int cell = (Random.Next(0, Size - 1) / 2) * 2 + 1;
            int x = 0, y = 0;

            switch (Random.Next(1, 4))
            {
                case 1:
                    y = cell;
                    break;
                case 2:
                    x = cell;
                    break;
                case 3:
                    x = Size - 1;
                    y = cell;
                    break;
                case 4:
                    x = cell;
                    y = Size - 1;
                    break;
            }
            Map[x, y].RoomType = RoomTypes.Passage;
            Map[x, y].IsExit = true;
        }

        /// <summary>
        /// Iterates through each chamber in the maze and stores the exiatance od neighbours as booleans.
        /// </summary>
        void SetNighbours()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (Map[x, y].RoomType == RoomTypes.Chamber)
                    {
                        Map[x, y].Neighbours[0] = Map[x - 1, y].RoomType == RoomTypes.Passage;
                        Map[x, y].Neighbours[1] = Map[x, y + 1].RoomType == RoomTypes.Passage;
                        Map[x, y].Neighbours[2] = Map[x + 1, y].RoomType == RoomTypes.Passage;
                        Map[x, y].Neighbours[3] = Map[x, y - 1].RoomType == RoomTypes.Passage;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Object for each room and it's related properties
    /// </summary>
    public class Room
    {
        /// <summary>
        /// The x, y coordinates of the room location.
        /// </summary>
        public Tuple<int, int> Location { get; private set; }
        /// <summary>
        /// Enum value of the room type.
        /// </summary>
        public RoomTypes RoomType { get; set; }
        /// <summary>
        /// Human readable room name.
        /// </summary>
        public string RoomName { get; private set; }
        /// <summary>
        /// Boolean value of whether the room has already been visited.
        /// </summary>
        public bool Visited { get; set; }
        /// <summary>
        /// Boolean value of whether the room is an exit passage
        /// </summary>
        public bool IsExit { get; set; }
        /// <summary>
        /// Boolean array of whether the neighbouring cells are a passage.
        /// In order of north, east, south, west
        /// </summary>
        public bool[] Neighbours { get; set; }

        /// <summary>
        /// Initalizes the room. Parameters are the x, y coordinates, the room type, and whether is is an exit passage.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="isExit"></param>
        /// <param name="roomType"></param>
        public Room(int x, int y, bool isExit, RoomTypes roomType)
        {
            Location = new Tuple<int, int>(x, y);
            IsExit = isExit;
            RoomType = roomType;
            RoomName = GetRoomName();
            Neighbours = new bool[] { false, false, false, false };
        }

        /// <summary>
        /// Initalizes the room. Parameters are the x, y coordinates, and the room type
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="roomType"></param>
        public Room(int x, int y, RoomTypes roomType) : this(x, y, false, roomType)
        {
        }

        /// <summary>
        /// Method returns the human readable name based on the room type.
        /// </summary>
        /// <returns></returns>
        string GetRoomName()
        {
            switch (RoomType)
            {
                case RoomTypes.Chamber:
                    return "Chamber";
                case RoomTypes.Passage:
                    return "Passage";
                case RoomTypes.Wall:
                    return "Wall";
                default:
                    return "";
            }
        }
    }

    /// <summary>
    /// Enumerator of room types found in the maze.
    /// </summary>
    public enum RoomTypes
    {
        Chamber,
        Passage,
        Wall
    }
}
