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
        private Reservation reservation;

        public ActionResult Index()
        {
            return View(CreateIndexViewModel());
        }

        //Hier halen we alle informatie op die nodig is voor de concert index
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
                //Hier halen we even de locatie uit de eerste dayConcert omdat ze per dag hetzelfde zijn
                indexfestivalday.Location = dayConcerts[0].Location.Name;

                vm.IndexFestivalDays.Add(indexfestivalday);
            }

            return vm;
        }

        //Nadat op de index op een dag is geklikt kom die hier binnen en wordt voor die dag een festivalDay gemaakt
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

            PassParToutDay passParToutDay = new PassParToutDay
            {
                Day = vm.Day
            };

            vm.PassParToutDay = passParToutDay;


            return View(vm);
        }

        [HttpPost]
        public ActionResult Reservation(ReservationViewModel reservationViewModel)
        {
            if(Session["Reservation"] != null)
            {
                reservation = (Reservation)Session["Reservation"];
            }
            else
            {
                reservation = new Reservation();
            }

            List<Ticket> tickets = new List<Ticket>();
            foreach(ConcertTicket concertTicket in reservationViewModel.ConcertTickets)
            {
                if(concertTicket.Ticket.Count != 0)
                {
                    Ticket ticket = new Ticket
                    {
                        EventId = concertTicket.Ticket.EventId,
                        Event = eventRepository.GetEvent(concertTicket.Ticket.EventId),
                        Count = concertTicket.Ticket.Count
                    };

                    tickets.Add(ticket);
                }
            }

            if (reservation.PassParToutDays == null)
            {
                List<PassParToutDay> passParToutDaysList = new List<PassParToutDay>
                {
                    reservationViewModel.PassParToutDay
                };
                reservation.PassParToutDays = passParToutDaysList;
            }
            else
            {
                bool found = false;
                foreach(PassParToutDay passParTout in reservation.PassParToutDays)
                {
                    if (passParTout.Day.Equals(reservationViewModel.Day))
                    {
                        passParTout.Count += reservationViewModel.ConcertTickets.Count;
                        found = true;
                        break;
                    }
                    else
                    {
                        found = false;
                    }
                }

                //Loop door alle passPartouts in de session heen, als hij er niet tussen staat voeg hem toe
                if (!found)
                {
                    reservation.PassParToutDays.Add(reservationViewModel.PassParToutDay);
                }
            }

            if (reservation.PassParToutWeek == null)
            {
                reservation.PassParToutWeek = reservationViewModel.PassParToutWeek;
            }
            else
            {
                reservation.PassParToutWeek.count += reservationViewModel.PassParToutWeek.count;
            }
            
            if (reservation.Tickets == null)
            {
                reservation.Tickets = tickets.ToList<object>();
            }
            else
            {
                reservation.Tickets.AddRange(tickets);
            }

            Session["Reservation"] = reservation;


            return RedirectToAction("Basket", "Home");
        }
    }
}