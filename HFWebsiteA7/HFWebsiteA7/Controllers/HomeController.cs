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
        IOrderRepository orderRepository = new OrderRepository();

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

        public ActionResult Checkout()
        {
            CheckoutViewModel vm = new CheckoutViewModel();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Checkout(CheckoutViewModel vm)
        {
            string email = vm.Email;
            string code = GenerateCode();

            Order newOrder = new Order
            {
                EmailAddress = email,
                Code = code
            };

            orderRepository.AddOrder(newOrder);

            Order order = orderRepository.GetOrderByEmailCode(email, code);
            


            return RedirectToAction("Conformation", "Home");
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

                    totalPrice += vm.ParToutWeek.count * weekPrice;
                }

                vm.TotalPrice = (double)totalPrice;
            }

            return View(vm);
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