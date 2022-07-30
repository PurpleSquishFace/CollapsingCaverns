using System;
using Entity_Engine_Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synoptic_Project;

namespace Synoptic_Project___Tests
{
    [TestClass]
    public class Threat_Interactions
    {
        [TestMethod]
        public void FightTrollTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Troll")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }

                }
            }
            logic.Entities.Fight(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Health;
            //Assert
            Assert.AreEqual(result, 250);
        }

        [TestMethod]
        public void FightSkeletonTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Skeleton")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }

                }
            }
            logic.Entities.Fight(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Health;
            //Assert
            Assert.AreEqual(result, 400);
        }

        [TestMethod]
        public void FightBatTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Bat")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }

                }
            }
            logic.Entities.Fight(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Health;
            //Assert
            Assert.AreEqual(result, 450);
        }

        [TestMethod]
        public void LureTrollGoldTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Gold(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Troll")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Gold.Value;
            //Assert
            Assert.AreEqual(result, 100);
        }

        [TestMethod]
        public void LureSkeletonGoldTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Gold(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Skeleton")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Gold.Value;
            //Assert
            Assert.AreEqual(result, 50);
        }

        [TestMethod]
        public void LureBatGoldTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Gold(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Bat")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Gold.Value;
            //Assert
            Assert.AreEqual(result, 75);
        }

        [TestMethod]
        public void LureTrollSilverTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Silver(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Troll")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Silver.Value;
            //Assert
            Assert.AreEqual(result, 100);
        }

        [TestMethod]
        public void LureSkeletonSilverTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Silver(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Skeleton")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Silver.Value;
            //Assert
            Assert.AreEqual(result, 35);
        }

        [TestMethod]
        public void LureBatSilverTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Silver(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Bat")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Silver.Value;
            //Assert
            Assert.AreEqual(result, 68);
        }

        [TestMethod]
        public void LureTrollBronzeTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Bronze(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Troll")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Bronze.Value;
            //Assert
            Assert.AreEqual(result, 110);
        }

        [TestMethod]
        public void LureSkeletonBronzeTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Bronze(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Skeleton")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Bronze.Value;
            //Assert
            Assert.AreEqual(result, 30);
        }

        [TestMethod]
        public void LureBatBronzeTest()
        {
            //Assign
            var logic = new Maze_Logic(5, "TestName");
            //Act
            logic.Entities.Player.UpdateTreasure(new Bronze(100));
            var loc = new Tuple<int, int>(0, 0);
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        foreach (var item in logic.Entities.Items[i, j])
                        {
                            if (item.Name == "Bat")
                            {
                                loc = new Tuple<int, int>(i, j);
                                break;
                            }
                        }
                    }
                }
            }
            logic.Entities.Lure(loc.Item1, loc.Item2);
            var result = logic.Entities.Player.Bronze.Value;
            //Assert
            Assert.AreEqual(result, 70);
        }
    }
}
