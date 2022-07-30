using System;
using System.Collections.Generic;
using System.Text;

namespace Entity_Engine_Library
{
    /// <summary>
    /// Main entity class
    /// </summary>
    public class Entities
    {
        public int ListID { get; set; }
        public string Name { get; set; }
        public Tuple<int,int> Location { get; set; }
        public EntityType Type { get; set; }

    }
    /// <summary>
    /// Main treasure class. Inherits from Entities
    /// </summary>
    public class ITreasure : Entities
    {
        public int Value { get; set; }
    }
    /// <summary>
    /// Gold treasure class. Inherits from ITreasure.
    /// </summary>
    public class Gold : ITreasure
    {
        public Gold(int x, int y, int value) : this(value)
        {
            this.Location = new Tuple<int, int>(x, y);
        }

        public Gold(int value)
        {
            this.Value = value;
            this.Name = "Gold";
            this.Type = EntityType.Treasure;
        }
    }
    /// <summary>
    /// Silver treasure class. Inherits from ITreasure.
    /// </summary>
    public class Silver : ITreasure
    {
        public Silver(int x, int y, int value) : this(value)
        {
            this.Location = new Tuple<int, int>(x, y);
            
        }

        public Silver(int value)
        {
            this.Value = value;
            this.Name = "Silver";
            this.Type = EntityType.Treasure;
        }
    }
    /// <summary>
    /// Bronze treasure class. Inherits from ITreasure.
    /// </summary>
    public class Bronze : ITreasure
    {
        public Bronze(int x, int y, int value) : this(value)
        {
            this.Location = new Tuple<int,int>(x,y);           
        }

        public Bronze(int value)
        {
            this.Value = value;
            this.Name = "Bronze";
            this.Type = EntityType.Treasure;
        }
    }
    /// <summary>
    /// Coin treasure class. Inherits from ITreasure.
    /// </summary>
    public class Coin : ITreasure
    {
        public int Amount { get; set; }

        public Coin()
        {
            this.Amount = 0;
            this.Value = 0;
            this.Name = "Coin";
            this.Type = EntityType.Coin;
        }
    }
    /// <summary>
    /// Main threat class. Inherits from Entities
    /// </summary>
    public class IThreat : Entities
    {
        public int Strength { get; set; }
        public int Lure_Gold { get; set; }
        public int Lure_Silver { get; set; }
        public int Lure_Bronze { get; set; }

        public IThreat() { } 

        public IThreat(int x, int y, int strength)
        {
            this.Location = new Tuple<int, int>(x, y);
            this.Strength = strength;
            this.Type = EntityType.Threat;

            double percent = strength / 100.0;

            Lure_Gold = Convert.ToInt32(Math.Round(percent * 50));
            Lure_Silver = Convert.ToInt32(Math.Round(percent * 65));
            Lure_Bronze = Convert.ToInt32(Math.Round(percent * 80));
        }
    }
    /// <summary>
    /// Troll threat class. Inherits from IThreat.
    /// </summary>
    public class Troll : IThreat
    {
        public Troll()
        {
        }

        public Troll(int x, int y, int strength): base(x, y, strength)
        {
            this.Name = "Troll";
        }
    }
    /// <summary>
    /// Skeleton threat class. Inherits from IThreat.
    /// </summary>
    class Skeleton : IThreat
    {
        public Skeleton(int x, int y, int strength) : base(x, y, strength)
        {
            this.Name = "Skeleton";
        }
    }
    /// <summary>
    /// Bat threat class. Inherits from IThreat.
    /// </summary>
    class Bat : IThreat
    {
        public Bat(int x, int y, int strength): base(x,y,strength)
        {
            this.Name = "Bat";
        }
    }
    /// <summary>
    /// Main Player class. Holds player specific data. Inherits from Entities.
    /// </summary>
    public class Player : Entities
    {
        /// <summary>
        /// The gold a player has.
        /// </summary>
        public Gold Gold { get; private set; }
        /// <summary>
        /// The silver a player has.
        /// </summary>
        public Silver Silver { get; private set; }
        /// <summary>
        /// The bronze a player has.
        /// </summary>
        public Bronze Bronze { get; private set; }
        /// <summary>
        /// The health of a player.
        /// </summary>
        public int Health { get; private set; }
        /// <summary>
        /// Initializes player object.
        /// Parameter name sets the name property.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name) : this(name, new Tuple<int,int>(1,1))
        {          
        }
        /// <summary>
        /// Initializes player object.
        /// Parameter name sets the name property.
        /// Parameter location sets the x, y location of the player.
        /// All other properties of the player are initialized.
        /// </summary>
        /// <param name="name"></param>
        public Player(string name, Tuple<int,int> location)
        {
            this.Name = name;
            this.Location = location;
            this.Type = EntityType.Player;
            this.Gold = new Gold(0);
            this.Silver = new Silver(0);
            this.Bronze = new Bronze(10);
            this.Health = 500;
        }
        /// <summary>
        /// Updates the player's treasure properties.
        /// Parameter is the treasure object to be added to the player.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateTreasure(ITreasure item)
        {
            int value = item.Value;
            switch (item.Name)
            {
                case "Gold":
                    Gold.Value += value;
                    break;
                case "Silver":
                    Silver.Value += value;
                    break;
                case "Bronze":
                    Bronze.Value += value;
                    break;
            }

            return true;
        }
        /// <summary>
        /// Updates the player's treasure properties.
        /// Parameter is the type of treasure and the value to be taken away from the player.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateTreasure(string type, int value)
        {
            switch (type)
            {
                case "Gold":
                    Gold.Value -= value;
                    break;
                case "Silver":
                    Silver.Value -= value;
                    break;
                case "Bronze":
                    Bronze.Value -= value;
                    break;
            }

            return true;
        }
        /// <summary>
        /// Updates the player's health property.
        /// Parameter is the amount of damage to be taken away from the player's health.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool UpdateHealth(int damage)
        {
            this.Health -= damage;
            return true;
        }
    }

    /// <summary>
    /// Enumerator of entity types found in the maze.
    /// </summary>
    public enum EntityType
    {
        Treasure,
        Threat,
        Player,
        Coin
    }
}
