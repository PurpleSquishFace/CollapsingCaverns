using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity_Engine_Library;
using Newtonsoft.Json;

namespace Synoptic_Project.Controllers
{
    public class PlayController : Controller
    {
        Maze_Logic Logic
        {
            get { return (Maze_Logic)HttpContext.Session["Maze_Logic"]; }
            set { HttpContext.Session["Maze_Logic"] = value; }
        }

        public ActionResult Maze()
        {
            Preferences p = new Preferences();
            p.LoadFile();
            Logic = new Maze_Logic(p.Map_Size, p.PlayerName);

            var location = Logic.Maze.CurrentLocation;
            ViewBag.Location = location;
            ViewBag.North = Logic.Maze.CurrentNeighbours[0];
            ViewBag.East = Logic.Maze.CurrentNeighbours[1];
            ViewBag.South = Logic.Maze.CurrentNeighbours[2];
            ViewBag.West = Logic.Maze.CurrentNeighbours[3];
            ViewBag.Entities = JsonConvert.SerializeObject(Logic.Entities.Items[location.Item1, location.Item1]);

            return View();
        }

        public ActionResult Move(int x, int y, string direction)
        {
            if (Logic.Move(x, y, direction)) return Json(new { complete = true }, JsonRequestBehavior.AllowGet);

            return RedirectToAction("RoomData");   
        }

        public ActionResult RoomData()
        {
            var param = new
            {
                complete = false,
                x = Logic.Maze.CurrentLocation.Item1,
                y = Logic.Maze.CurrentLocation.Item2,
                north = Logic.Maze.CurrentNeighbours[0],
                east = Logic.Maze.CurrentNeighbours[1],
                south = Logic.Maze.CurrentNeighbours[2],
                west = Logic.Maze.CurrentNeighbours[3],
                entities = Logic.Entities.Items[Logic.Maze.CurrentLocation.Item1, Logic.Maze.CurrentLocation.Item2],
                player = Logic.Entities.Player
            };

            return Json(param, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Loot(int x, int y)
        {
            Logic.LootTreasure(x, y);

            return RedirectToAction("RoomData");
        }

        public bool DropCoin(int x, int y)
        {
            return Logic.Entities.Drop(x, y); ;
        }

        public bool Lure(int x, int y)
        {
            return Logic.Entities.Lure(x, y);
        }

        public bool Fight(int x, int y)
        {
            return Logic.Entities.Fight(x, y);
        }

        public ActionResult Complete()
        {
            ViewBag.Player = Logic.Entities.Player;

            return View();
        }
    }
}