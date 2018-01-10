using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;
using HFWebsiteA7.Repositories.Classes;
using HFWebsiteA7.ViewModels;

namespace HFWebsiteA7.Controllers
{
    public class ConcertsController : Controller
    {
        private IConcertsRepository concertRepository = new ConcertRepository();
        private IDayRepository dayRepository = new DayRepository();
        private IEventRepository eventRepository = new EventRepository();

        public ActionResult Index()
        {
            return View(CreateIndexViewModel());
        }

        private IndexViewModel CreateIndexViewModel()
        {
            IndexViewModel vm = new IndexViewModel
            {
                IndexFestivalDays = new List<IndexFestivalDay>()
            };

            foreach (Day day in dayRepository.GetAllDays().ToList())
            {
                IndexFestivalDay indexfestivalday = new IndexFestivalDay
                {
                    Concerts = new List<Concert>()
                };

                List<Concert> dayConcerts = concertRepository.GetConcertsByDay(day.Id);
                indexfestivalday.Concerts.AddRange(dayConcerts);
                indexfestivalday.Date = day.Date;
                indexfestivalday.Day = day;
                indexfestivalday.Location = dayConcerts[0].Location.Name;

                vm.IndexFestivalDays.Add(indexfestivalday);
            }

            return vm;
        }

        public ActionResult ConcertOverview(int dayId)
        {
            Day day = dayRepository.GetDay(dayId);

            return View(concertRepository.CreateFestivalDay(day));
        }

        public ActionResult Reservation(int dayId, int concertId)
        {
            ReservationViewModel vm = new ReservationViewModel
            {
                Day = dayRepository.GetDay(dayId).Name
            };

            vm.ConcertTickets = new List<ConcertTicket>();
            int i = 0;
            foreach (Concert concert in concertRepository.GetConcertsByDay(dayId))
            {
                ConcertTicket concertTicket = new ConcertTicket
                {
                    Ticket = new Ticket
                    {
                        Id = i,
                        EventId = concert.EventId,
                        Event = eventRepository.GetEvent(concert.EventId),
                        Count = 0
                    },
                    Concert = concert
                };
                if(concertTicket.Concert.BandId == concertId)
                {
                    concertTicket.Selected = true;
                }
                vm.ConcertTickets.Add(concertTicket);
                i++;
            }
            
            return View(vm);
        }

        [HttpPost]
        public ActionResult Reservation(ReservationViewModel reservationViewModel)
        {
            List<Ticket> tickets = new List<Ticket>();
            foreach(ConcertTicket concertTicket in reservationViewModel.ConcertTickets)
            {
                if(concertTicket.Ticket.Count != 0)
                {
                    Ticket ticket = new Ticket
                    {
                        EventId = concertTicket.Ticket.EventId,
                        Count = concertTicket.Ticket.Count
                    };

                    tickets.Add(ticket);
                }
            }

            if (Session["Tickets"] == null)
            {
                Session["Tickets"] = tickets;
            }
            else
            {
                List<Ticket> ticketsInSession = (List<Ticket>) Session["Tickets"];
                ticketsInSession.AddRange(tickets);
                Session["Tickets"] = ticketsInSession;
            }


            return RedirectToAction("Basket", "Home");
        }
    }
}