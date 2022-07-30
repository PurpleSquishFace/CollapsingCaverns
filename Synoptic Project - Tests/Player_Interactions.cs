using System;
using Entity_Engine_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synoptic_Project;

namespace Synoptic_Project___Tests
{
    [TestClass]
    public class Player_Interactions
    {
        [TestMethod]
        public void PlayerNameTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var result = logic.Entities.Player.Name;
            //Assert
            Assert.AreEqual(result, "TestName");
        }

        [TestMethod]
        public void PlayerBronzeTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var result = logic.Entities.Player.Bronze.Value;
            //Assert
            Assert.AreEqual(result, 10);
        }

        [TestMethod]
        public void PlayerSilverTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var result = logic.Entities.Player.Silver.Value;
            //Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void PlayerGoldTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var result = logic.Entities.Player.Gold.Value;
            //Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void UpdatePlayerTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.UpdatePlayer(5, 5);
            var result = logic.Entities.Player.Location;
            //Assert
            Assert.AreEqual(result, new Tuple<int, int>(5, 5));
        }

        [TestMethod]
        public void UpdateTreasureGoldTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var treasure = new Gold(25);
            logic.Entities.Player.UpdateTreasure(treasure);
            var result = logic.Entities.Player.Gold.Value;
            //Assert
            Assert.AreEqual(result, 25);
        }

        [TestMethod]
        public void UpdateTreasureSilverTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var treasure = new Silver(25);
            logic.Entities.Player.UpdateTreasure(treasure);
            var result = logic.Entities.Player.Silver.Value;
            //Assert
            Assert.AreEqual(result, 25);
        }

        [TestMethod]
        public void UpdateTreasureBronzeTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var treasure = new Bronze(25);
            logic.Entities.Player.UpdateTreasure(treasure);
            var result = logic.Entities.Player.Bronze.Value;
            //Assert
            Assert.AreEqual(result, 35);
        }

        [TestMethod]
        public void UpdateHealthTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateHealth(50);
            var result = logic.Entities.Player.Health;
            //Assert
            Assert.AreEqual(result, 450);
        }
    }
}
