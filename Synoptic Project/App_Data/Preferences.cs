using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze_Engine_Library;
using Entity_Engine_Library;
using System.IO;
using Newtonsoft.Json;
using System.Web;

namespace Synoptic_Project
{
    class Preferences
    {
        /// <summary>
        /// Current player's name
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// Size of the maze (the number of rooms in each direction)
        /// </summary>
        public int Map_Size { get; set; }
        /// <summary>
        /// The difficulty of the maze.
        /// </summary>
        public Difficulty Difficulty { get; set; }

        /// <summary>
        /// Intializes the default preferences for the maze.
        /// </summary>
        public Preferences()
        {
            this.PlayerName = "Player";
            this.Map_Size = 5;
            this.Difficulty = Difficulty.Medium;
        }
        /// <summary>
        /// Initializes the custom preference for the maze.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="difficulty"></param>
        public Preferences(string name, int size, int difficulty)
        {
            this.PlayerName = name;
            this.Map_Size = size;
            this.Difficulty = (Difficulty)difficulty;
        }
        /// <summary>
        /// Saves the preference properties to text file, in Json format.
        /// </summary>
        /// <returns></returns>
        public bool WriteFile()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/preferences.txt");
            using (StreamWriter stream = new StreamWriter(path))
            {
                string preferencesJson = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                stream.Write(preferencesJson);
            }

            return true;
        }
        /// <summary>
        /// Loads the preference properites from a text file, converts them from Json and sets the properties of the class.
        /// </summary>
        /// <returns></returns>
        public bool LoadFile()
        {
            string path = HttpContext.Current.Server.MapPath("~/App_Data/preferences.txt");
            using (StreamReader file = File.OpenText(path))
            {
                Preferences p = JsonConvert.DeserializeObject<Preferences>(file.ReadToEnd(), new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                this.PlayerName = p.PlayerName;
                this.Map_Size = p.Map_Size;
                this.Difficulty = p.Difficulty;

                return true;
            }
        }
    }
    /// <summary>
    /// Enumerator of maze difficulty.
    /// </summary>
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
}
