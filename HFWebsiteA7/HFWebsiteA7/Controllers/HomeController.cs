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
            if((List<Ticket>)Session["Tickets"] != null)
            {
                List<Ticket> tickets = new List<Ticket>();

                foreach (Ticket ticket in tickets)
                {
                    ticket.Event = eventRepository.GetEvent(ticket.EventId);
                }

                BasketViewModel vm = new BasketViewModel
                {
                    Tickets = tickets
                };

                return View(vm);

            }
            else
            {
                return View();
            }
        }
    }
}