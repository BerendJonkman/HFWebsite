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

        // GET: Concerts
        public ActionResult Index()
        {
            IndexViewModel vm = new IndexViewModel();

            FillViewModel(vm);

            return View(vm);
        }

        private void FillViewModel(IndexViewModel vm)
        {
            vm.IndexFestivalDays = new List<IndexFestivalDay>();
            List<Day> days = dayRepository.GetAllDays().ToList();

            foreach (Day day in days)
            {
                IndexFestivalDay indexfestivalday = new IndexFestivalDay
                {
                    Concerts = new List<Concert>()
                };

                switch (day.Name)
                {
                    case "Thursday":
                        FillIndexFestivalDay(indexfestivalday, day, vm);
                        break;
                    case "Friday":
                        FillIndexFestivalDay(indexfestivalday, day, vm);
                        break;
                    case "Saturday":
                        FillIndexFestivalDay(indexfestivalday, day, vm);
                        break;
                    case "Sunday":
                        FillIndexFestivalDay(indexfestivalday, day, vm);
                        break;
                }
            }
        }

        private void FillIndexFestivalDay(IndexFestivalDay indexfestivalday, Day day, IndexViewModel vm)
        {
            List<Concert> dayConcerts = concertRepository.GetConcertsByDay(day.Name);
            indexfestivalday.Concerts.AddRange(dayConcerts);
            indexfestivalday.Date = day.Date;
            indexfestivalday.Day = day.Name;
            indexfestivalday.Location = dayConcerts[0].Location.Name;

            vm.IndexFestivalDays.Add(indexfestivalday);
        }

        public ActionResult Thursday()
        {
            return View(FillDayViewModel(concertRepository.GetConcertsByDay("Thursday")));
        }

        public ActionResult Friday()
        {
            return View(FillDayViewModel(concertRepository.GetConcertsByDay("Friday")));
        }

        public ActionResult Saturday()
        {
            return View(FillDayViewModel(concertRepository.GetConcertsByDay("Saturday")));
        }

        public ActionResult Sunday()
        {        
            return View(FillDayViewModel(concertRepository.GetConcertsByDay("Sunday")));
        }

        private DayViewModel FillDayViewModel(List<Concert> concerts)
        {
            DayViewModel vm = new DayViewModel
            {
                FestivalDay = new FestivalDay
                {
                    MainConcertList = new List<Concert>(),
                    SecondConcertList = new List<Concert>()
                }
            };

            foreach (Concert concert in concerts)
            {
                if (concert.Hall.Name.Equals("Main Hall"))
                {
                    vm.FestivalDay.MainConcertList.Add(concert);
                }
                else
                {
                    vm.FestivalDay.SecondConcertList.Add(concert);
                }
            }

            return vm;
        }

        [HttpGet]
        public ActionResult Reservation(string Day)
        {
            ReservationViewModel vm = new ReservationViewModel
            {
                Day = Day
            };

            vm.Concerts = new List<Concert>(concertRepository.GetConcertsByDay(Day));
            
            return View(vm);
        }
    }
}