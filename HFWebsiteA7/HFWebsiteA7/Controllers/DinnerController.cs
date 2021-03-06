﻿using System;
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
        private Restaurant restaurant = new Restaurant();
        private IDinnerSessionRepository dinnerSessionRepository = new DinnerSessionRepository();
        private IRestaurantRepository restaurantRepository = new RestaurantRepository();
        private IRestaurantFoodTypeRepository restaurantFoodTypeRepository = new RestaurantFoodTypeRepository();
        private IDayRepository dayRepository = new DayRepository();
        private IEventRepository eventRepository = new EventRepository();
        private List<Restaurant> tempRestaurantList = new List<Restaurant>();
        private Reservation reservation;


        // GET: Dinner
        public ActionResult Index()
        {
            return View(CreateIndexViewModel());
        }

        private RestaurantViewModel CreateIndexViewModel()
        {
            RestaurantViewModel vm = new RestaurantViewModel
            {
                RestaurantList = new List<RestaurantAndFoodType>()
            };

            //list for foreach to loop through
            tempRestaurantList = restaurantRepository.GetAllRestaurants().ToList();
            List<String> FoodTypes = new List<string>();

            //retrieve foodtypelist from repository and concatenate these into a string for viewing
            foreach (var item in tempRestaurantList) {
                IEnumerable<FoodType> foodTypeList = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(item.Id);
                RestaurantAndFoodType restaurandAndFoodType = new RestaurantAndFoodType();
                string foodTypes = "";

                foreach (var foodItem in foodTypeList)
                {
                    if (foodTypes == "")
                    {
                        foodTypes = foodItem.Name;
                    }
                    else
                    {
                        foodTypes += ", " + foodItem.Name;
                    }
                }
                //add restaurant and foodtypes to model restaurantfoodtype and add restaurantandfoodtype to the restaurantlist in the ViewModel 
                restaurandAndFoodType.restaurant = item;
                restaurandAndFoodType.foodType = foodTypes;
                vm.RestaurantList.Add(restaurandAndFoodType);
            }
            return vm;
        }

        //process reservation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation(FormCollection collection)
        {
            //check if session is already set and set of new reservation or load from session
            if (Session["Reservation"] != null)
            {
                reservation = (Reservation)Session["Reservation"];
            }
            else
            {
                reservation = new Reservation();
            }

            
            DinnerSession selectedDinnerSession = new DinnerSession();

            //get input from user from the Collection that's passed from the view
            int amount = Convert.ToInt32(collection.Get("ticket-amount"));
            int dayId = Convert.ToInt32(collection.Get("day"));
            DateTime timeSlot = Convert.ToDateTime(collection.Get("timeslot"));
            int restaurantId = Convert.ToInt32(collection.Get("restaurantId"));
            string remarks = collection.Get("remarks");

            Restaurant restaurant = restaurantRepository.GetRestaurant(restaurantId);

            //get dinnerSessions by restaurantId and timeslot the customer selected
            List<DinnerSession> dinnerSession = dinnerSessionRepository.getDinnerSessionsByRestaurantAndStartTime(restaurantId, timeSlot).ToList();

            //check for each session in the dinnersession list if the dayId matches the selected dayId by the customer
            foreach (DinnerSession session in dinnerSession)
            {
                if(session.DayId == dayId)
                {
                    selectedDinnerSession = session;
                }
            }
            
            //fill dinnerTicket and PreTicket
            DinnerTicket dinnerTicket = new DinnerTicket
            {
                Ticket = new PreTicket
                {
                    Id = 1,
                    EventId = selectedDinnerSession.EventId,
                    Event = eventRepository.GetEvent(selectedDinnerSession.EventId),
                    Count = amount
                },
                Restaurant = restaurant,
                Remarks = remarks,
                Count = amount,
                Id = selectedDinnerSession.EventId
            };

            //create new list of Tickets if reservation doesn't contain any.
            if (reservation.Tickets == null)
            {
                reservation.Tickets = new List<BaseTicket>();
            }

            reservation.Tickets.Add(dinnerTicket);

            Session["Reservation"] = reservation;

            return RedirectToAction("Basket", "Home");
        }


        public ActionResult DetailsPage(int Id)
        {
            DinnerDetails dinnerDetails = new DinnerDetails();
            DinnerSession dinnerSession = dinnerSessionRepository.GetDinnerSessionByRestaurantId(Id);

            IEnumerable<FoodType> foodTypeList = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(Id);

            string foodTypes = foodTypesAsString(Id);

            

            dinnerDetails.foodtype = foodTypesAsString(Id);
            dinnerDetails.restaurant = restaurantRepository.GetRestaurant(Id);
            dinnerDetails.duration = (dinnerSession.Duration * 45).ToString();
            dinnerDetails.startTimes = retrieveStarttimes(Id, true).startTimeString;

            return View(dinnerDetails);
        }

        public ActionResult OrderPage(int Id)
        {
            DinnerOrder dinnerOrder = new DinnerOrder();
            List<Day> days = new List<Day>();
            Restaurant restaurant = restaurantRepository.GetRestaurant(Id);
            List<DinnerSession> dinnerSessions = dinnerSessionRepository.GetAllDinnerSessionsByRestaurantId(Id).ToList();
            
            //filter all days from dinnerSessions per restaurant so only new days are add to days list
            foreach(DinnerSession dinnerSession in dinnerSessions) {
                if (!days.Contains(dayRepository.GetDay(dinnerSession.DayId))) {
                    days.Add(dayRepository.GetDay(dinnerSession.DayId));
                }
            }
            
            dinnerOrder.days = days;
            dinnerOrder.restaurant = restaurant;
            dinnerOrder.timeslot = retrieveStarttimes(Id, false).startTimeSession.ToList();

            return View(dinnerOrder);
        }

        //Method to retrieve foodtypes by restaurantId and return them as a concatenated string
        private string foodTypesAsString(Int32 restaurantId)
        {
            IEnumerable<FoodType> foodTypeList = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(restaurantId);

            string foodTypes = "";

            foreach (var foodItem in foodTypeList)
            {
                if (foodTypes == "")
                {
                    foodTypes = foodItem.Name;
                }
                else
                {
                    foodTypes += ", " + foodItem.Name;
                }
            }
            return foodTypes;
        }

        //Method to retrieve the starttimes.
        //First parameter is an int restaurantId of which you want to retrieve the start times
        //Second parameter is a Bool. True returns a string of the start time hours. False returns a list of dinnersessions containing the different start times.
        public StartTimes retrieveStarttimes(int restaurantId, bool stringOrListAsSession)
        {
            List<DinnerSession> dinnerSessions = dinnerSessionRepository.GetAllDinnerSessionsByRestaurantId(restaurantId).ToList();
            List<DateTime> timeCheck = new List<DateTime>();

            string startTimesString = "";
            List<DinnerSession> startTimesDateTime = new List<DinnerSession>();

            StartTimes returnValue = new StartTimes();

            foreach (DinnerSession session in dinnerSessions)
            {
                if (startTimesString == "")
                {
                    if (!timeCheck.Contains(session.StartTime))
                    {
                        if (stringOrListAsSession == true)
                        {
                            timeCheck.Add(session.StartTime);
                            startTimesString = session.StartTime.ToString("HH:mm");
                        }
                        else
                        {
                            timeCheck.Add(session.StartTime);
                            startTimesDateTime.Add(session);
                        }
                    }
                }
                else
                {
                    if (!timeCheck.Contains(session.StartTime))
                    {
                        if (stringOrListAsSession == true)
                        {
                            timeCheck.Add(session.StartTime);
                            startTimesString += ", " + session.StartTime.ToString("HH:mm");
                        }
                        else
                        {
                            timeCheck.Add(session.StartTime);
                            startTimesDateTime.Add(session);
                        }
                    }
                }
            }
            returnValue.startTimeSession = startTimesDateTime;
            returnValue.startTimeString = startTimesString;

            return returnValue;
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
