using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HFWebsiteA7.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace HFWebsiteA7.Controllers
{
    public class HomeController : Controller
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        private List<Band> bands = new List<Band>();
        private List<Event> events = new List<Event>();
        private List<Concert> concerts = new List<Concert>();

        private List<Restaurant> restaurants = new List<Restaurant>();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}