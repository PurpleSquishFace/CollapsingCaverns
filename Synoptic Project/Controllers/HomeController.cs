using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synoptic_Project.Controllers
{
    public class HomeController : Controller
    {
        Preferences Preferences
        {
            get { return (Preferences)HttpContext.Session["Preferences"]; }
            set { HttpContext.Session["Preferences"] = value; }
        }


        public ActionResult Index()
        {
            if (Preferences == null) Preferences = new Preferences();
            
            return View();
        }

        public bool SaveSettings(string name, int size, int difficulty)
        {
            Preferences = new Preferences(name, size, difficulty);
            Preferences.WriteFile();

            return true;
        }

        
    }
}