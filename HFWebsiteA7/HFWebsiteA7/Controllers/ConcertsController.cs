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

namespace HFWebsiteA7.Controllers
{
    public class ConcertsController : Controller
    {
        private IConcertsRepository concertRepository = new ConcertRepository();
        private IDayRepository dayRepository = new DayRepository();

        private List<Concert> thursdayConcerts= new List<Concert>();
        private List<Concert> fridayConcerts = new List<Concert>();
        private List<Concert> saturdayConcerts = new List<Concert>();
        private List<Concert> sundayConcerts = new List<Concert>();

        // GET: Concerts
        public ActionResult Concerts()
        {
            List<Concert> concerts = concertRepository.GetAllConcerts().ToList();
            List<Day> days = dayRepository.GetAllDays().ToList();

            FillConcertLists();

            JazzViewModel vm = new JazzViewModel();

            FillFestivalDays(vm);
            SplitConcertListIntoTwoHalls(vm.FestivalDays);

            return View(vm);
        }

        private void SplitConcertListIntoTwoHalls(List<FestivalDay> festivalDays)
        {
            foreach (FestivalDay festivalDay in festivalDays)
            {
                festivalDay.MainConcertList = new List<Concert>();
                festivalDay.SecondConcertList = new List<Concert>();

                foreach (Concert concert in festivalDay.Concerts)
                {
                    if(concert.Hall.Name.Equals("Main Hall"))
                    {
                        festivalDay.MainConcertList.Add(concert);
                    }
                    else
                    {
                        festivalDay.SecondConcertList.Add(concert);
                    }
                }
            }
        }

        private void FillConcertLists()
        {
            List<Concert> concerts = concertRepository.GetAllConcerts().ToList();
            List<Day> days = dayRepository.GetAllDays().ToList();

            foreach (Concert concert in concerts)
            {
                switch (concert.Day.Name)
                {
                    case "Thursday":
                        thursdayConcerts.Add(concert);
                        break;
                    case "Friday":
                        fridayConcerts.Add(concert);
                        break;
                    case "Saturday":
                        saturdayConcerts.Add(concert);
                        break;
                    case "Sunday":
                        sundayConcerts.Add(concert);
                        break;
                }
            }
        }

        private void FillFestivalDays(JazzViewModel vm)
        {
            vm.FestivalDays = new List<FestivalDay>();
            List<Concert> concerts = concertRepository.GetAllConcerts().ToList();
            List<Day> days = dayRepository.GetAllDays().ToList();

            foreach (Day day in days)
            {
                FestivalDay festivalday = new FestivalDay
                {
                    Concerts = new List<Concert>()
                };

                switch (day.Name)
                {
                    case "Thursday":
                        festivalday.Concerts.AddRange(thursdayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = thursdayConcerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                    case "Friday":
                        festivalday.Concerts.AddRange(fridayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = fridayConcerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                    case "Saturday":
                        festivalday.Concerts.AddRange(saturdayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = saturdayConcerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                    case "Sunday":
                        festivalday.Concerts.AddRange(sundayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = sundayConcerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                }
            }
        }

        public ActionResult Thursday()
        {
            FillConcertLists();

            JazzViewModel vm = new JazzViewModel();

            FillFestivalDays(vm);
            SplitConcertListIntoTwoHalls(vm.FestivalDays);

            return View(vm);
        }

        public ActionResult Friday()
        {
           FillConcertLists();

            JazzViewModel vm = new JazzViewModel();

            FillFestivalDays(vm);
            SplitConcertListIntoTwoHalls(vm.FestivalDays);

            return View(vm);
        }

        public ActionResult Saturday()
        {
            FillConcertLists();

            JazzViewModel vm = new JazzViewModel();

            FillFestivalDays(vm);
            SplitConcertListIntoTwoHalls(vm.FestivalDays);

            return View(vm);
        }

        public ActionResult Sunday()
        {
            FillConcertLists();

            JazzViewModel vm = new JazzViewModel();

            FillFestivalDays(vm);
            SplitConcertListIntoTwoHalls(vm.FestivalDays);

            return View(vm);
        }

        [HttpGet]
        public ActionResult Reservation(string Day)
        {
            ReservationViewModel vm = new ReservationViewModel
            {
                Day = Day
            };
            vm.Concerts = new List<Concert>();
            FillConcertLists();

            switch(Day)
            {
                case "Thursday":
                    vm.Concerts = thursdayConcerts;
                    break;
                case "Friday":
                    vm.Concerts = fridayConcerts;
                    break;
                case "Saturday":
                    vm.Concerts = saturdayConcerts;
                    break;
                case "Sunday":
                    vm.Concerts = sundayConcerts;
                    break;
            }
            
            return View(vm);
        }
    }
}
