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


            if ((List<Ticket>)Session["Tickets"] != null)
            {
                List<Ticket> tickets = new List<Ticket>();

                foreach (Ticket ticket in tickets)
                {
                    ticket.Event = eventRepository.GetEvent(ticket.EventId);
                }

                vm.Tickets = tickets;
            }

            if((List<PassParToutDay>)Session["PassParToutDay"] != null)
            {
                vm.Partoutdays = (List<PassParToutDay>)Session["PassParToutDay"];
            }

            if ((PassParToutWeek)Session["PassParToutWeek"] != null)
            {
                vm.ParToutWeek = (PassParToutWeek)Session["PassParToutWeek"];
            }

            return View(vm);
        }
    }
}