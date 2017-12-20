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
    public class DinnerController : Controller
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        private IDinnerSessionRepository dinnerSessionRepository = new DinnerSessionRepository();


        // GET: Dinner
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dinner/Details/5
       

        // GET: Dinner/Create
        public ActionResult Create()
        {
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name");
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Description");
            return View();
        }

        // POST: Dinner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,DayId,AvailableSeats,Discriminator,RestaurantId,Duration,StartTime")] DinnerSession dinnerSession)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(dinnerSession);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", dinnerSession.DayId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Description", dinnerSession.RestaurantId);
            return View(dinnerSession);
        }

        // GET: Dinner/Edit/5
        
        

        // POST: Dinner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,DayId,AvailableSeats,Discriminator,RestaurantId,Duration,StartTime")] DinnerSession dinnerSession)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dinnerSession).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DayId = new SelectList(db.Days, "Id", "Name", dinnerSession.DayId);
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Description", dinnerSession.RestaurantId);
            return View(dinnerSession);
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
