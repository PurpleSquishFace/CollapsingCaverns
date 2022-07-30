using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Engine_Library
{
    public class Maze_Engine
    {
        /// <summary>
        /// Holds the maze, and all attributed properties.
        /// </summary>
        public Room[,] Map { get; private set; }
        /// <summary>
        /// The Array is the number of rooms * 2 +1.
        /// This is so the map accomodates each room and the walls in between.
        /// </summary>
        public int MapSize { get; private set; }
        /// <summary>
        /// Tracks the current location of the player within the maze. Made up of an x, y coordinate.
        /// </summary>
        public Tuple<int,int> CurrentLocation { get; set; }
        /// <summary>
        /// Boolean array of whether the neighbours of the current room are passages. In order of north, east, south, west. 
        /// </summary>
        public bool[] CurrentNeighbours { get; set; }
        /// <summary>
        /// An instance of the helper methods class.
        /// </summary>
        Helper_Methods Helper { get; set; }

        /// <summary>
        /// Initialize the Maze Engine.
        /// Generates a maze based on given size, and initialize properties ready for use.
        /// </summary>
        /// <param name="size"></param>
        public Maze_Engine(int size)
        {
            Maze_Generator maze = new Maze_Generator(size);
            Helper = new Helper_Methods();
            Map = maze.Map;
            MapSize = maze.Size;
            CurrentLocation = SetLocation();
            CurrentNeighbours = Map[CurrentLocation.Item1, CurrentLocation.Item2].Neighbours;
        }

        /// <summary>
        /// Updates a room to mark it as visited.
        /// Parameters are the coordinates of the room.
        /// Returns a boolean of whether it was successful.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns a boolean of whether it was successful.</returns>
        public bool UpdateVisitedRoom(int x, int y)
        {
            Map[x, y].Visited = true;
            return true;
        }

        /// <summary>
        /// Updates the current location the player is in within the map.
        /// Parameter are the player's coordinates
        /// Returns a boolean of whether it was successful.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns a boolean of whether it was successful.</returns>
        public bool UpdateLocation(int x, int y)
        {
            CurrentLocation = new Tuple<int, int>(x, y);
            CurrentNeighbours = Map[x, y].Neighbours;
            return true;
        }

        /// <summary>
        /// Checks if a given room is an exit passage or not.
        /// Parameters are the coordinates of the passage being checked.
        /// Returns a boolean of whether it is an exit.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns a boolean of whether it is an exit.</returns>
        public bool CheckExit(int x, int y)
        {
            return Map[x, y].IsExit;
        }

        /// <summary>
        /// Gets a random pair of coordinates for a player's inital location.
        /// Returns a tuple made up of an x and y coordinate
        /// </summary>
        /// <returns>Returns a tuple made up of an x and y coordinate</returns>
        private Tuple<int,int> SetLocation()
        {
            return new Tuple<int, int>(Helper.RandomNumber(MapSize), Helper.RandomNumber(MapSize));
        }
    }
}
