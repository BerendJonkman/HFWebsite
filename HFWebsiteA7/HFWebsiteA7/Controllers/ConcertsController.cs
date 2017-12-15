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

                List<Concert> dayConcerts = concertRepository.GetConcertsByDay(day.Name);
                indexfestivalday.Concerts.AddRange(dayConcerts);
                indexfestivalday.Date = day.Date;
                indexfestivalday.Day = day.Name;
                indexfestivalday.Location = dayConcerts[0].Location.Name;

                vm.IndexFestivalDays.Add(indexfestivalday);
            }

            return vm;
        }

        public ActionResult Thursday()
        {
            return View(MakeDayViewModel("Thursday"));
        }

        public ActionResult Friday()
        {
            return View(MakeDayViewModel("Friday"));
        }

        public ActionResult Saturday()
        {
            return View(MakeDayViewModel("Saturday"));
        }

        public ActionResult Sunday()
        {        
            return View(MakeDayViewModel("Sunday"));
        }

        private DayViewModel MakeDayViewModel(string day)
        {
            return new DayViewModel { FestivalDay = concertRepository.CreateFestivalDay(day) };
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