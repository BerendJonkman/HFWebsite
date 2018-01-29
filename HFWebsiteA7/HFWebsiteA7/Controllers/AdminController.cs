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
using System.Web.Security;
using HFWebsiteA7.Repositories;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace HFWebsiteA7.Controllers
{
    public class AdminController : Controller
    {
        private IDinnerSessionRepository dinnerSessionRepository = new DinnerSessionRepository();
        private IRestaurantFoodTypeRepository restaurantFoodTypeRepository = new RestaurantFoodTypeRepository();
        private IRestaurantRepository restaurantRepository = new RestaurantRepository();
        private IConcertsRepository concertRepository = new ConcertRepository();
        private IBandRepository bandRepository = new BandRepository();
        private ILocationRepository locationRepository = new LocationRepository();
        private IAdminUserRepository adminUserRepository = new AdminUserRepository();
        private IEventRepository eventRepository = new EventRepository();
        private IHallRepository hallRepository = new HallRepository();
        private IDayRepository dayRepository = new DayRepository();
        private IFoodTypeRepository foodTypeRepository = new FoodTypeRepository();
        private string bandImagePath = "~/Content/Content-images/JazzImages/BandImages/";
        private string restaurantImagePath = "~/Content/Content-images/RestaurantImages/Restaurants/";

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AdminUser user)
        {
            if (ModelState.IsValid)
            {
                var adminUser = adminUserRepository.GetAdminUserByUser(user);
                if (adminUser != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    Session["loggidin_account"] = user;

                    return RedirectToAction("AdminSelection");

                }
                else
                {
                    ModelState.AddModelError("login-error", "The username or password provided is incorrect.");
                }
            }
            return View();
        }

        public ActionResult AdminSelection()
        {
            return View();
        }

        public ActionResult AdminEventEdit(EventTypeEnum type)
        {
            var adminEventEditViewModel = CreateAdminEventEditViewModel(type);
            return View(adminEventEditViewModel);
        }

        [HttpPost]
        public ActionResult CreateBand(AdminBand adminBand)
        {
            var type = EventTypeEnum.Bands;
            if (ModelState.IsValid)
            {
                var filename = CreateFileName(adminBand.Band.Name);
                var path = Path.Combine(Server.MapPath(bandImagePath), filename);
                adminBand.Band.ImagePath = bandImagePath + filename;
                Image sourceimage = Image.FromStream(adminBand.File.InputStream);
                sourceimage.Save(path, ImageFormat.Jpeg);

                bandRepository.AddBand(adminBand.Band);
                var adminEventEditViewModel = CreateAdminEventEditViewModel(type);
                return View("AdminEventEdit", CreateAdminEventEditViewModel(type));
            }
            return View("AdminEventEdit", CreateAdminEventEditViewModel(type));
        }

        [HttpPost]
        public ActionResult CreateConcert(AdminConcert adminConcert)
        {
            var type = EventTypeEnum.Concerts;
            adminConcert.Concert.TableType = "Concert";
            adminConcert.Concert.AvailableSeats = hallRepository.GetHall(adminConcert.Concert.HallId).Seats;
            if (ModelState.IsValid)
            {

                // eventRepository.AddEvent(newEvent);
                adminConcert.Concert.EventId = eventRepository.GetLastEvent().EventId;
                concertRepository.AddConcert(adminConcert.Concert);
            }
            return View("AdminEventEdit", CreateAdminEventEditViewModel(type));
        }

        [HttpPost]
        public ActionResult CreateLocation(Location location)
        {
            var type = EventTypeEnum.Locations;
            if (ModelState.IsValid)
            {
                locationRepository.AddLocation(location);
            }
            return View("AdminEventEdit", CreateAdminEventEditViewModel(type));
        }

        [HttpPost]
        public ActionResult CreateAdminRestaurant(AdminRestaurant adminRestaurant)
        {
            var type = EventTypeEnum.Restaurants;

            if (ModelState.IsValid)
            {
                var filename = CreateFileName(adminRestaurant.Restaurant.Name);
                var path = Path.Combine(Server.MapPath(restaurantImagePath), filename);
                adminRestaurant.Restaurant.ImagePath = restaurantImagePath + filename;
                Image sourceimage = Image.FromStream(adminRestaurant.File.InputStream);
                sourceimage.Save(path, ImageFormat.Jpeg);

                restaurantRepository.AddRestaurant(adminRestaurant.Restaurant);
                foreach(var day in dayRepository.GetAllDays())
                {
                    var startTime = adminRestaurant.StartTime;
                    for (var i = 0; i < adminRestaurant.Sessions; i++)
                    {
                        var dinnerSession = new DinnerSession();
                        dinnerSession.DayId = day.Id;
                        dinnerSession.AvailableSeats = adminRestaurant.Restaurant.Seats;
                        dinnerSession.RestaurantId = adminRestaurant.Restaurant.Id;
                        dinnerSession.TableType = "DinnerSessions";
                        if(i != 0)
                        {
                            startTime.AddHours((double)adminRestaurant.Duration);
                        }
                        dinnerSession.StartTime = startTime;
                    }
                }
            }
            return View("AdminEventEdit", CreateAdminEventEditViewModel(type));
        }

        private string CreateFileName(string name)
        {
            return Regex.Replace(name, @"\s+", "") + ".jpg";
        }

        public AdminEventEditViewModel CreateAdminEventEditViewModel(EventTypeEnum type)
        {
            AdminEventEditViewModel vm = new AdminEventEditViewModel
            {
                EventType = type
            };
            var events = eventRepository.GetAllEvents();
            switch (type)
            {
                case EventTypeEnum.Bands:
                    vm.ObjectList = bandRepository.GetAllBands().ToList<object>();
                    vm.AdminBand = new AdminBand();
                    break;
                case EventTypeEnum.Concerts:
                    vm.ObjectList = concertRepository.GetAllConcerts().ToList<object>();
                    vm.AdminConcert = CreateAdminConcert();
                    break;
                case EventTypeEnum.Restaurants:
                    vm.ObjectList = CreateAdminRestaurantList();
                    vm.AdminRestaurant = CreateAdminRestaurant();
                    break;
                case EventTypeEnum.Locations:
                    vm.Location = new Location();
                    vm.ObjectList = locationRepository.GetAllLocations().ToList<object>();
                    break;
            }

            return vm;
        }

        private AdminRestaurant CreateAdminRestaurant()
        {
            var adminRestaurant = new AdminRestaurant();

            var locationList = locationRepository.GetAllLocations().Select(x =>
                                new SelectListItem()
                                {
                                    Text = $"{ x.Street} {x.HouseNumber}, {x.ZipCode} {x.City}",
                                    Value = x.Id.ToString()
                                });
            var foodTypeList = foodTypeRepository.GetAllFoodTypes().Select(x =>
                                new SelectListItem()
                                {
                                    Text = x.Name.ToString(),
                                    Value = x.Id.ToString()
                                });
            adminRestaurant.LocationList = locationList;
            adminRestaurant.FoodTypeSelectList = foodTypeList;
            return adminRestaurant;
        }

        private List<object> CreateAdminRestaurantList()
        {
            var dinnerSessions = dinnerSessionRepository.GetAllDinnerSessions();
            var restaurantList = restaurantRepository.GetAllRestaurants();
            var adminRestaurantList = new List<AdminRestaurant>();
            foreach (var restaurant in restaurantList)
            {
                var adminRestaurant = new AdminRestaurant();
                adminRestaurant.Restaurant = restaurant;
                var sessionCount = 0;
                var first = true;
                foreach (var dinnerSession in dinnerSessions)
                {
                    if (dinnerSession.RestaurantId == restaurant.Id && dinnerSession.DayId == 1)
                    {
                        if (first)
                        {
                            first = false;
                            adminRestaurant.StartTime = dinnerSession.StartTime;
                            adminRestaurant.Duration = dinnerSession.Duration;
                        }
                        sessionCount++;
                    }
                }
                adminRestaurant.Sessions = sessionCount;
                adminRestaurant.FoodTypes = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(restaurant.Id).ToList();
                adminRestaurantList.Add(adminRestaurant);
            }

            return adminRestaurantList.ToList<object>();
        }

        private AdminConcert CreateAdminConcert()
        {
            var bandList = bandRepository.GetAllBands().Select(x =>
                                 new SelectListItem()
                                 {
                                     Text = x.Name.ToString(),
                                     Value = x.Id.ToString()
                                 });
            var hallList = hallRepository.GetAllHalls().Select(x =>
                           new SelectListItem()
                           {
                               Text = x.Name.ToString(),
                               Value = x.Id.ToString()
                           });
            var locationList = locationRepository.GetAllLocations().Select(x =>
                          new SelectListItem()
                          {
                              Text = x.Name.ToString(),
                              Value = x.Id.ToString()
                          });
            var dayList = dayRepository.GetAllDays().Select(x =>
                          new SelectListItem()
                          {
                              Text = x.Name.ToString(),
                              Value = x.Id.ToString()
                          });
            var adminConcert = new AdminConcert()
            {
                BandList = bandList,
                LocationList = locationList,
                HallList = hallList,
                DayList = dayList,
                Concert = new Concert()
            };

            return adminConcert;
        }

        [HttpGet]
        public JsonResult GetBand(int bandId)
        {
            Band adminBand = bandRepository.GetBand(bandId);

            return Json(adminBand, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLocation(int locationId)
        {
            Location location = locationRepository.GetLocation(locationId);
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetConcert(int concertId)
        {
            Concert concert = concertRepository.GetConcert(concertId);
            return Json(concert, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        JsonResult GetAdminRestaurant(int restaurantId)
        {
            var adminRestaurant = new AdminRestaurant();
            adminRestaurant.Restaurant = restaurantRepository.GetRestaurant(restaurantId);
            adminRestaurant.FoodTypes = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(restaurantId).ToList();
            adminRestaurant.Sessions = 0;
            var first = true;
            foreach (var dinnerSession in dinnerSessionRepository.GetAllDinnerSessions())
            {
                if (dinnerSession.RestaurantId == adminRestaurant.Restaurant.Id && dinnerSession.DayId == 1)
                {
                    if (first)
                    {
                        first = false;
                        adminRestaurant.StartTime = dinnerSession.StartTime;
                        adminRestaurant.Duration = dinnerSession.Duration;
                    }
                    adminRestaurant.Sessions++;
                }
            }

            return Json(adminRestaurant, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void UpdateBand(int bandId, string name, string description)
        {
            Band band = new Band()
            {
                Id = bandId,
                Name = name,
                Description = description,
                ImagePath = bandImagePath + CreateFileName(name)
            };
            bandRepository.UpdateBand(band);
        }

        [HttpPost]
        public void UpdateConcert(int eventId, int locationId, int hallId, decimal duration, string startTime)
        {
            DateTime timeStartTime = Convert.ToDateTime(startTime);
            Debug.WriteLine(timeStartTime);
            int noOfSeats = hallRepository.GetHall(hallId).Seats;

            var concert = concertRepository.GetConcert(eventId);
            concert.LocationId = locationId;
            concert.HallId = hallId;
            concert.Duration = duration;
            concert.StartTime = timeStartTime;
            concert.AvailableSeats = noOfSeats;

            concertRepository.UpdateConcert(concert);
        }

        [HttpPost]
        public void UpdateLocation(int locationId, string name, string street, string houseNumber, string city, string zipCode)
        {
            var location = new Location()
            {
                Id = locationId,
                Name = name,
                Street = street,
                HouseNumber = houseNumber,
                City = city,
                ZipCode = zipCode
            };

            locationRepository.UpdateLocation(location);
        }

        [HttpPost]
        public void UpdateAdminRestaurant(int restaurantId, int availableSeats, string name, int locationId, decimal price, decimal reducedPrice, int stars, int seats, string description, int sessions, string startTime, int[] foodTypeArray, decimal duration)
        {
            Restaurant restaurant = new Restaurant()
            {
                Id = restaurantId,
                Description = description,
                LocationId = locationId,
                Name = name,
                Price = price,
                Seats = seats,
                Stars = stars,
                ReducedPrice = reducedPrice,
                ImagePath = restaurantImagePath + CreateFileName(name)
            };
            restaurantRepository.UpdateRestaurant(restaurant);

            UpdateDinnersSessions(duration, startTime, restaurantId, availableSeats, sessions);

            UpdateRestaurantFoodTypes(foodTypeArray, restaurantId);
        }

        private void UpdateDinnersSessions(decimal Duration, string StartTime, int RestaurantId, int AvailableSeats, int Sessions)
        {
            var dinnerSessions = dinnerSessionRepository.GetAllDinnerSessionsByRestaurantId(RestaurantId);
            var startDateTime = Convert.ToDateTime(StartTime);
            if (Sessions == dinnerSessions.Count())
            {
                foreach (var dinnerSession in dinnerSessions)
                {
                    dinnerSession.Duration = Duration;
                    dinnerSession.RestaurantId = RestaurantId;
                    dinnerSession.AvailableSeats = AvailableSeats;
                    if (!dinnerSession.Equals(dinnerSessions.First()))
                    {
                        startDateTime.AddHours((double)Duration);
                    }
                    dinnerSession.StartTime = startDateTime;
                }
            }
            else
            {
                foreach (var dinnerSession in dinnerSessions)
                {
                    dinnerSessionRepository.DeleteDinnerSession(dinnerSession);
                }
                foreach (var day in dayRepository.GetAllDays())
                {
                    for (var i = 0; i < Sessions; i++)
                    {
                        var dinnerSession = new DinnerSession()
                        {
                            AvailableSeats = AvailableSeats,
                            DayId = day.Id,
                            Duration = Duration,
                            RestaurantId = RestaurantId,
                            TableType = "DinnerSessions"
                        };

                        if (i != 0)
                        {
                            startDateTime.AddHours((double)Duration);
                        }

                        dinnerSession.StartTime = startDateTime;

                        dinnerSessionRepository.AddDinnerSession(dinnerSession);
                    }
                }
            }
        }

        private void UpdateRestaurantFoodTypes(int[] foodTypeArray, int restaurantId)
        {
            foreach (var restaurantFoodType in restaurantFoodTypeRepository.GetRestaurantFoodTypesByRestaurantId(restaurantId))
            {
                restaurantFoodTypeRepository.DeleteRestaurantFoodType(restaurantFoodType);
            }

            foreach (var id in foodTypeArray)
            {
                var restaurantFoodType = new RestaurantFoodType()
                {
                    FoodTypeId = id,
                    RestaurantId = restaurantId
                };

                restaurantFoodTypeRepository.AddRestaurantFoodType(restaurantFoodType);
            }
        }

        [HttpPost]
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath(bandImagePath), fileName);

                Image sourceimage = Image.FromStream(file.InputStream);
                sourceimage.Save(path, ImageFormat.Jpeg);
            }
        }

        [HttpGet]
        public void RemoveBand(int bandId)
        {
            Band band = bandRepository.GetBand(bandId);
            bandRepository.removeBand(band);

            string fullPath = Request.MapPath(band.ImagePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
