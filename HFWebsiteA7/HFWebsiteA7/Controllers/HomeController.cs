using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HFWebsiteA7.Models;
using System.Data.SqlClient;
using System.Configuration;
using HFWebsiteA7.ViewModels;
using HFWebsiteA7.Repositories.Interfaces;
using HFWebsiteA7.Repositories.Classes;

namespace HFWebsiteA7.Controllers
{
    public class HomeController : Controller
    {
        IEventRepository eventRepository = new EventRepository();
        IConcertsRepository concertsRepository = new ConcertRepository();

        private Reservation reservation;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ContactSend()
        {
            return View();
        }

        public ActionResult Basket()
        {
            BasketViewModel vm = new BasketViewModel();

            if((Reservation)Session["Reservation"] != null)
            {
                reservation = (Reservation)Session["Reservation"];
            }
            
            if (reservation != null)
            {
                if (reservation.Tickets != null)
                {
                    vm.Tickets = reservation.Tickets;
                }

                if (reservation.PassParToutDays != null)
                {
                    vm.Partoutdays = reservation.PassParToutDays;
                }

                if (reservation.PassParToutWeek != null)
                {
                    vm.ParToutWeek = reservation.PassParToutWeek;
                }
            }

            return View(vm);
        }
    }
}