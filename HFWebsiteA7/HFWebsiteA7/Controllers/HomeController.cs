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
using System.Text.RegularExpressions;

namespace HFWebsiteA7.Controllers
{
    public class HomeController : Controller
    {
        IEventRepository eventRepository = new EventRepository();
        IConcertsRepository concertsRepository = new ConcertRepository();
        IPassPartoutTypeRepository passPartoutTypeRepository = new PassPartoutTypeRepository();
        IPassPartoutOrderRepository passPartoutOrderRepository = new PassPartoutOrderRepository();
        IOrderRepository orderRepository = new OrderRepository();
        ITicketRepository ticketRepository = new TicketRepository();
        IDayRepository dayRepository = new DayRepository();

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

        public ActionResult PersonalAgenda()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonalAgenda(PersonalAgendaViewModel vm)
        {
            Session["PersonalAgenda"] = null;

            if (ModelState.IsValid)
            {
                Order order = orderRepository.GetOrderByEmailCode(vm.Email, vm.Code);

                if (order != null)
                {
                    List<Ticket> concertTickets = ticketRepository.GetTicketsByOrderId(order.Id).ToList();
                    if (concertTickets != null)
                    {
                        vm.EventIdList = concertTickets.Select(x => x.EventId).Distinct().ToList();
                    }

                    List<PassPartoutOrder> passParToutTickets = passPartoutOrderRepository.GetPassParToutByOrderId(order.Id).ToList();
                    if (passParToutTickets != null)
                    {
                        vm.PassPartoutTypeList = passParToutTickets.Select(x => x.PassPartoutId).Distinct().ToList();
                    }
                }
                else
                {
                    ModelState.AddModelError("notFoundError", "No order was found with this email and code");
                }

                Session["PersonalAgenda"] = vm;
                return RedirectToAction("ShowPersonalAgenda", "Home");
            }

            return View();
        }

        public ActionResult ShowPersonalAgenda()
        {
            if(Session["PersonalAgenda"] != null)
            {
                PersonalAgendaViewModel vm = (PersonalAgendaViewModel)Session["PersonalAgenda"];
                if(vm.EventIdList != null)
                {
                    vm.EventList = new List<Event>();
                    vm.ConcertList = new List<Concert>();

                    foreach (int e in vm.EventIdList)
                    {
                        Event evnt = eventRepository.GetEvent(e);
                        vm.EventList.Add(evnt);
                        if(evnt.TableType.Equals("Concert"))
                        {
                            vm.ConcertList.Add(concertsRepository.GetConcert(e));
                        }
                    }
                }

                return View(vm);
            }

            return View();
        }

        public ActionResult Checkout()
        {
            CheckoutViewModel vm = new CheckoutViewModel();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string email = vm.Email;
                string code = GenerateCode();
                List<object> tickets = new List<object>();
                List<PassParToutDay> passParToutDays = new List<PassParToutDay>();
                PassParToutWeek passParToutWeek = new PassParToutWeek();

                if ((Reservation)Session["Reservation"] != null)
                {
                    reservation = (Reservation)Session["Reservation"];
                }

                if (reservation != null)
                {
                    if (reservation.Tickets != null)
                    {
                        tickets = reservation.Tickets;
                    }
                    if (reservation.PassParToutDays != null)
                    {
                        passParToutDays = reservation.PassParToutDays;
                    }
                    if (reservation.PassParToutWeek != null)
                    {
                        passParToutWeek = reservation.PassParToutWeek;
                    }
                }

                Order newOrder = new Order
                {
                    EmailAddress = email,
                    Code = code
                };

                orderRepository.AddOrder(newOrder);

                Order order = orderRepository.GetOrderByEmailCode(email, code);

                foreach (object ob in tickets)
                {
                    if (ob is ConcertTicket ct)
                    {
                        Ticket ticket = new Ticket();
                        PreTicket pt = ct.Ticket;

                        ticket.EventId = pt.EventId;
                        ticket.OrderId = order.Id;

                        for (int i = 0; i < ct.Ticket.Count; i++)
                        {
                            ticketRepository.AddTicket(ticket);
                        }

                        eventRepository.LowerAvailableSeats(ct.Ticket.EventId, ct.Ticket.Count);
                    }
                }

                foreach (PassParToutDay pd in passParToutDays)
                {
                    List<PassPartoutType> ppttList = passPartoutTypeRepository.GetAllPassPartoutType().ToList();

                    foreach (PassPartoutType type in ppttList)
                    {
                        if (type.Name.Equals(pd.Day))
                        {
                            pd.Id = type.Id;
                            break;
                        }
                    }

                    PassPartoutOrder passPartoutOrder = new PassPartoutOrder
                    {
                        OrderId = order.Id,
                        PassPartoutId = pd.Id
                    };

                    for (int i = 0; i < pd.Count; i++)
                    {
                        passPartoutOrderRepository.AddPassPartoutOrder(passPartoutOrder);
                    }

                    Day day = dayRepository.GetDayByName(pd.Day);

                    eventRepository.LowerAvailableSeatsforDay(day.Id, pd.Count);
                }

                if (passParToutWeek.Count > 0)
                {
                    List<PassPartoutType> ppttList = passPartoutTypeRepository.GetAllPassPartoutType().ToList();

                    foreach (PassPartoutType type in ppttList)
                    {
                        if (type.Name.Equals(passParToutWeek.Type))
                        {
                            passParToutWeek.Id = type.Id;
                            break;
                        }
                    }

                    PassPartoutOrder passPartoutOrderWeek = new PassPartoutOrder
                    {
                        OrderId = order.Id,
                        PassPartoutId = passParToutWeek.Id
                    };

                    for (int i = 0; i < passParToutWeek.Count; i++)
                    {
                        passPartoutOrderRepository.AddPassPartoutOrder(passPartoutOrderWeek);
                    }

                    eventRepository.LowerAllAvailableSeats(passParToutWeek.Count);

                }


                //Leeg de session want de items zijn in de database gezet
                Session["Reservation"] = null;

                return RedirectToAction("Conformation", "Home");
            }

            return View();
        }

        public ActionResult Conformation()
        {

            return View();
        }

        public ActionResult Basket()
        {
            BasketViewModel vm = new BasketViewModel();

            if ((Reservation)Session["Reservation"] != null)
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

                decimal totalPrice = 0;

                if (vm.Tickets != null)
                {
                    foreach (ConcertTicket ct in vm.Tickets)
                    {
                        totalPrice += ct.Ticket.Count * ct.Concert.Hall.Price;
                    }
                }

                if (vm.Partoutdays != null)
                {
                    foreach (PassParToutDay pd in vm.Partoutdays)
                    {
                        decimal dayPrice = passPartoutTypeRepository.GetPassPartoutType(1).Price;
                        totalPrice += pd.Count * dayPrice;
                    }
                }

                if (vm.ParToutWeek != null)
                {
                    decimal weekPrice = passPartoutTypeRepository.GetPassPartoutType(4).Price;

                    totalPrice += vm.ParToutWeek.Count * weekPrice;
                }

                vm.TotalPrice = (double)totalPrice;
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Basket(BasketViewModel vmNew)
        {
            

            return RedirectToAction("Go to checkout", "Checkout");
        }

        private string GenerateCode()
        {
            Random rn = new Random();
            string charsToUse = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            MatchEvaluator RandomChar = delegate (Match m)
            {
                return charsToUse[rn.Next(charsToUse.Length)].ToString();
            };

            return Regex.Replace("XXXXXX", "X", RandomChar);
        }
    }
}