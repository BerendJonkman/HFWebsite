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

            if (reservation.Tickets != null)
            {
                foreach (Ticket ticket in reservation.Tickets)
                {
                    ConcertTicket concertTicket = new ConcertTicket();
                    if (ticket.Event.Discriminator.Equals("Concert"))
                    {
                        bool found = false;
                        if (vm.Tickets != null)
                        {
                            foreach (Ticket t in vm.Tickets)
                            {
                                concertTicket.Ticket = t;
                                concertTicket.Concert = concertsRepository.GetConcert(t.EventId);
                                if (ticket.EventId == t.EventId)
                                {
                                    found = true;
                                    t.Count += ticket.Count;
                                    break;
                                }
                            }
                            if (found)
                            {
                                vm.Tickets.Add(concertTicket);
                            }
                        }
                        else
                        {
                            vm.Tickets = new List<object>
                            {
                                ticket
                            };
                        }
                    }
                }
            }

            if(reservation.PassParToutDays != null)
            {
                vm.Partoutdays = reservation.PassParToutDays;
            }

            if (reservation.PassParToutWeek != null)
            {
                vm.ParToutWeek = reservation.PassParToutWeek;
            }

            return View(vm);
        }
    }
}