using System;
using System.Collections.Generic;

namespace Entity_Engine_Library
{
    public class Entity_Engine
    {
        /// <summary>
        /// Property for current player properties.
        /// </summary>
        public Player Player { get; set; }
        /// <summary>
        /// Array of the map, with each roomm represented as a list of it's entities for that room.
        /// </summary>
        public List<Entities>[,] Items { get; set; }
        /// <summary>
        /// Property for use with random elements of entity generation.
        /// </summary>
        private Random Random { get; set; }

        /// <summary>
        /// Initialize the Entity Engine.
        /// Generates all entities in the maze, and initialize properties ready for use.
        /// Size parameter sets the size of the array of entities.
        /// Player name parameter sets the name of the player entity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="playerName"></param>
        public Entity_Engine(int size, string playerName)
        {
            Items = new List<Entities>[size, size];
            Random = new Random();
            Player = new Player(playerName);

            for (int x = 0; x < size; x++)
            {   
                for (int y = 0; y < size; y++)
                {
                    if (x % 2 != 0 && y % 2 != 0)
                    {
                        var list = new List<Entities>();

                        list.Add(new Coin());

                        if (Random.Next(0, 100) < 80)   // Add treasure
                        {
                            // Gold
                            if (Random.Next(0, 100) <= 15) list.Add(new Gold(x, y, Random.Next(1, 50)));
                            // Silver
                            if (Random.Next(0, 100) > 15 && Random.Next(0, 100) <= 50) list.Add(new Silver(x, y, Random.Next(50, 100)));
                            // Bronze
                            if (Random.Next(0, 100) > 50) list.Add(new Bronze(x, y, Random.Next(100, 200)));
                        }

                        if (Random.Next(0, 100) < 70)   // Add threats
                        {
                            var random = Random.Next(0, 100);
                            if (random <= 15) list.Add(new Troll(x, y, 250));
                            if (random > 15 && random <= 50) list.Add(new Skeleton(x, y, 100));
                            if (random > 50) list.Add(new Bat(x, y, 50));
                        }

                        for (int i = 0; i < list.Count; i++) list[i].ListID = i;
                        
                        Items[x, y] = list;
                    }
                }
            }
        }
        /// <summary>
        /// Updates the player's current location.
        /// Parameters are the x, y coordinates of the location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool UpdatePlayer(int x, int y)
        {
            Player.Location = new Tuple<int, int>(x, y);
            return true;
        }
        /// <summary>
        /// Loots the room provided by the x, y coordinates.
        /// Adds the looted treasure to the player's properties.
        /// Returns true if successful.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Loot(int x, int y)
        {
            for (int i = 0; i < Items[x, y].Count; i++) Items[x, y][i].ListID = i;

            List<Entities> treasure = Items[x, y];
            List<int> ids = new List<int>();
            foreach (var item in treasure)
            {
                if (item.Type == EntityType.Treasure)
                {
                    Player.UpdateTreasure((ITreasure)item);
                    ids.Add(item.ListID);
                }
            }

            foreach (int id in ids)
            {
                for (int val = treasure.Count -1; val >= 0; val--)
                {
                    if (Items[x, y][val].ListID == id)
                    {
                        Items[x, y].Remove(Items[x, y][val]);
                    }
                }
            }
            
            return true;
        }
        /// <summary>
        /// Attemps to lure the threat in the room provided by the x, y coordinates.
        /// Tries using the player's gold value, then silver and finally bronze.
        /// Returns boolean value if successful or not.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Lure(int x, int y)
        {
            for (int i = 0; i < Items[x, y].Count; i++) Items[x, y][i].ListID = i;

            IThreat threat = new IThreat();
            foreach (var item in Items[x, y])
                if (item.Type == EntityType.Threat)
                    threat = (IThreat)item;

            if (Player.Gold.Value >= threat.Lure_Gold)
            {
                if (Player.UpdateTreasure("Gold", threat.Lure_Gold))
                {
                    Items[x, y].Remove(Items[x, y][threat.ListID]);
                    return true;
                }                
            }
            if (Player.Silver.Value >= threat.Lure_Silver)
            {
                if (Player.UpdateTreasure("Silver", threat.Lure_Silver))
                {
                    Items[x, y].Remove(Items[x, y][threat.ListID]);
                    return true;
                }
            }
            if (Player.Bronze.Value >= threat.Lure_Bronze)
            {
                if(Player.UpdateTreasure("Bronze", threat.Lure_Bronze))
                {
                    Items[x, y].Remove(Items[x, y][threat.ListID]);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Attemps to fight the threat in the room provided by the x, y coordinates.
        /// Compares player's health again threat's strength and acts accordingly.
        /// Returns boolean value if successful or not.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Fight(int x, int y)
        {
            for (int i = 0; i < Items[x, y].Count; i++) Items[x, y][i].ListID = i;

            IThreat threat = new IThreat();
            foreach (var item in Items[x, y]) if (item.Type == EntityType.Threat) threat = (IThreat)item;
                       

            if (Player.Health > threat.Strength)
            {
                if (Player.UpdateHealth(threat.Strength))
                {
                    Items[x, y].Remove(Items[x, y][threat.ListID]);
                    return true;
                }
            }

            threat.Strength -= Player.Health;
            Items[x, y][threat.ListID] = threat;
            Player = new Player(Player.Name, Player.Location);

            return false;
        }
        /// <summary>
        /// Increases the coin count by one of the room provided by the x, y coordinates.
        /// Represent the act of player dropping a coin in a room.
        /// Returns a boolean value if successful or not.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Drop(int x, int y)
        {
            if (Player.Gold.Value < 1) return false;

            Coin coin = new Coin();
            foreach (var item in Items[x, y]) if (item.Type == EntityType.Coin) coin = (Coin)item;

            coin.Amount += 1;
            Items[x, y][0] = coin;
            Player.Gold.Value -= 1;

            return true;
        }
    }
}
