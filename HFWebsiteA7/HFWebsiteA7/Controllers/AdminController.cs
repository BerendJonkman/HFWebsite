using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Controllers
{
    public class AdminController : Controller
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        private List<Event> events;

        // GET: Admin
        public ActionResult Index()
        {
            events = db.Events.ToList();
            return View(events);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name");
            return View();
        }

        public ActionResult Dinner()
        {
            var restaurants = db.Restaurants.ToList();
            var dinnerSessions = db.DinnerSessions.ToList();
            
            var adminRestaurants = new List<AdminRestaurant>();
            
            foreach(var restaurant in restaurants)
            {
                var foodTypes = db.RestaurantFoodType.Where(l => restaurant.Id == l.RestaurantId).ToList();
                var adminRestaurant = new AdminRestaurant
                {
                    Restaurant = restaurant,
                    Sessions = getSessions(dinnerSessions, restaurant.Id),

                };

            }
            DinnerAdminViewModel model = new DinnerAdminViewModel();
            model.restaurantList = restaurants;
            return View(model);
        }

        private int getSessions(List<DinnerSession> dinnerSessions, int restaurantId)
        {
            var sessionCount = 0;
            foreach(var dinnerSession in dinnerSessions)
            {
                if(dinnerSession.Day.Name == "Thursday" && dinnerSession.RestaurantId == restaurantId)
                {
                    sessionCount++;
                }
            }
            return sessionCount;
        }


        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,DayId,AvailableSeats")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", @event.DayId);
            return View(@event);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", @event.DayId);
            return View(@event);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,DayId,AvailableSeats")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", @event.DayId);
            return View(@event);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
