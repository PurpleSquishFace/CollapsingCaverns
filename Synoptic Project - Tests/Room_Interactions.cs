using System;
using Entity_Engine_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synoptic_Project;

namespace Synoptic_Project___Tests
{
    [TestClass]
    public class Room_Interactions
    {
        [TestMethod]
        public void MapSizeTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var result = logic.Maze.MapSize;
            //Assert
            Assert.AreEqual(result, 11);
        }

        [TestMethod]
        public void VisitedRoomTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Maze.UpdateVisitedRoom(5, 5);
            //Assert
            var result = logic.Maze.Map[5, 5].Visited;
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void CurrentLocationTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Maze.UpdateLocation(7, 7);
            //Assert
            var result = logic.Maze.CurrentLocation;
            Assert.AreEqual(result, new Tuple<int, int>(7, 7));
        }

        [TestMethod]
        public void IsExitTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            Tuple<int, int> exit = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (logic.Maze.Map[i, j].IsExit)
                    {
                        exit = new Tuple<int, int>(i, j);
                    }
                }
            }
            //Assert
            var result = logic.Maze.CheckExit(exit.Item1, exit.Item2);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void DropCoinNoGoldTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Drop(5, 5);
            Coin result = (Coin)logic.Entities.Items[5, 5][0];
            //Assert
            Assert.AreEqual(result.Amount, 0);
        }

        [TestMethod]
        public void DropCoinWithGoldTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.Gold.Value += 1;
            logic.Entities.Drop(5, 5);
            Coin result = (Coin)logic.Entities.Items[5, 5][0];
            //Assert
            Assert.AreEqual(result.Amount, 1);
        }

    }
}
