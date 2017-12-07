using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories;

namespace HFWebsiteA7.Controllers
{
    public class ConcertsController : Controller
    {
        private IConcertsRepository concertRepository = new ConcertRepository();

        private List<Concert> thursdayConcerts= new List<Concert>();
        private List<Concert> fridayConcerts = new List<Concert>();
        private List<Concert> saturdayConcerts = new List<Concert>();
        private List<Concert> sundayConcerts = new List<Concert>();

        // GET: Concerts
        public ActionResult Index()
        {
            var concerts = concertRepository.GetAllConcerts();
            var days = db.Days.ToList();
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

            JazzIndexViewModel vm = new JazzIndexViewModel();

            foreach (Day day in days)
            {
                FestivalDay festivalday = new FestivalDay();
                vm.FestivalDays = new List<FestivalDay>();
                festivalday.Concerts = new List<Concert>();
            
                switch (day.Name)
                {
                    case "Thursday":
                        festivalday.Concerts.AddRange(thursdayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = concerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                    case "Friday":
                        festivalday.Concerts.AddRange(fridayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = concerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                    case "Saturday":
                        festivalday.Concerts.AddRange(saturdayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = concerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                    case "Sunday":
                        festivalday.Concerts.AddRange(sundayConcerts);
                        festivalday.Date = day.Date;
                        festivalday.Day = day.Name;
                        festivalday.Location = concerts[0].Location.Name;

                        vm.FestivalDays.Add(festivalday);
                        break;
                }
            }

            return View(vm);
        }

        public ActionResult Thursday()
        {
            return View();
        }

        public ActionResult Friday()
        {
            return View();
        }

        public ActionResult Saturday()
        {
            return View();
        }

        public ActionResult Sunday()
        {
            return View();
        }

        public ActionResult Reservation()
        {
            return View();
        }

        // GET: Concerts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concert concert = db.Concerts.Find(id);
            if (concert == null)
            {
                return HttpNotFound();
            }
            return View(concert);
        }

        // GET: Concerts/Create
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name");
            ViewBag.BandId = new SelectList(db.Bands, "Id", "Name");
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name");
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name");
            return View();
        }

        // POST: Concerts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,DayId,AvailableSeats,LocationId,BandId,HallId,Duration,StartTime")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                db.Concerts.Add(concert);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", concert.DayId);
            ViewBag.BandId = new SelectList(db.Bands, "Id", "Name", concert.BandId);
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", concert.HallId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", concert.LocationId);
            return View(concert);
        }

        // GET: Concerts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concert concert = db.Concerts.Find(id);
            if (concert == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", concert.DayId);
            ViewBag.BandId = new SelectList(db.Bands, "Id", "Name", concert.BandId);
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", concert.HallId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", concert.LocationId);
            return View(concert);
        }

        // POST: Concerts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,DayId,AvailableSeats,LocationId,BandId,HallId,Duration,StartTime")] Concert concert)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", concert.DayId);
            ViewBag.BandId = new SelectList(db.Bands, "Id", "Name", concert.BandId);
            ViewBag.HallId = new SelectList(db.Halls, "Id", "Name", concert.HallId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Name", concert.LocationId);
            return View(concert);
        }

        // GET: Concerts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concert concert = db.Concerts.Find(id);
            if (concert == null)
            {
                return HttpNotFound();
            }
            return View(concert);
        }

        // POST: Concerts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concert concert = db.Concerts.Find(id);
            db.Concerts.Remove(concert);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
