using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_Engine_Library
{
    class Helper_Methods
    {
        /// <summary>
        /// Property of type Random for use with random elements of the maze.
        /// </summary>
        Random Random { get; set; }
        /// <summary>
        /// Initializes the helper method class
        /// </summary>
        public Helper_Methods()
        {
            Random = new Random();
        }
        /// <summary>
        /// Returns a random number between 0 and the provided integer.
        /// Logic ensures the number returned is a valid x or y coordinated of a room in the maze.
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int RandomNumber(int max)
        {
            int ans = Random.Next(0, max - 1);
            if (ans % 2 == 1) return ans;
            else
            {
                if (ans + 1 <= max)
                    return ans + 1;
                else if (ans - 1 >= 0)
                    return ans - 1;
                else return 0;
            }

        }
    }
}
