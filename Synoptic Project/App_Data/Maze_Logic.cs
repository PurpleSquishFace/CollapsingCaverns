using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Engine_Library;
using Entity_Engine_Library;

namespace Synoptic_Project
{
    /// <summary>
    /// Handles all logic between the User Interface and the two class libraries.
    /// </summary>
    public class Maze_Logic
    {
        /// <summary>
        /// An instance of the Maze_Engine class library.
        /// </summary>
        public Maze_Engine Maze { get; private set; }
        /// <summary>
        /// An instance of the Entity_Engine class library.
        /// </summary>
        public Entity_Engine Entities { get; private set; }
        /// <summary>
        /// Initializes the Maze_Logic class.
        /// The parameter size is used to initialize the Maze_Entitiy library.
        /// The playerName parameter is used to initialize the Entitiy_Engine library.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="playerName"></param>
        public Maze_Logic(int size, string playerName)
        {
            Maze = new Maze_Engine(size);
            Entities = new Entity_Engine(Maze.MapSize, playerName);
        }
        /// <summary>
        /// Moves the player by upading the player's location.
        /// The parameter are the x, y coordinates of the room the player is currently in, and the direction they are leaving the room in.
        /// Returns a boolean value of whether the player has exited the maze.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool Move(int x, int y, string direction)
        {
            Maze.UpdateVisitedRoom(x, y);
            int nx = x, ny = y;
            switch (direction)
            {
                case "north":
                    nx -= 2;
                    x -= 1;
                    break;
                case "east":
                    ny += 2;
                    y += 1;
                    break;
                case "south":
                    nx += 2;
                    x += 1;
                    break;
                case "west":
                    ny -= 2;
                    y -= 1;
                    break;
            }

            if (Maze.CheckExit(x, y)) return true;

            Maze.UpdateLocation(nx, ny);
            Entities.UpdatePlayer(nx,ny);
            return false;
        }
        /// <summary>
        /// Handles the player's loot room request.
        /// Passes the parameters, the x, y coordinates of the room, to the Entitiy_Engine library.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool LootTreasure(int x, int y)
        {
            return Entities.Loot(x, y);
        }
    }
}
