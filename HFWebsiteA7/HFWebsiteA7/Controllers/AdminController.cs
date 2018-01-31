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
using System.Text;

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
        private string restaurantImagePath = "~/Content/Content-images/RestaurantsImages/Restaurants/";

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //Logs in user if credentials ar correct
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

        //Logs out user
        [HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AdminSelection()
        {
            return View();
        }

        //Saves the select EventType to the Session and redirects to the select editpage
        [Authorize]
        [HttpGet]
        public ActionResult EventSelect(EventTypeEnum type)
        {
            Session["adminEventEditViewModel"] = CreateAdminEventEditViewModel(type);
            return RedirectToAction("AdminEventEdit");
        }

        //Gets model from the Session and creates edit View
        [Authorize]
        [HttpGet]
        public ActionResult AdminEventEdit()
        {
            var adminEventEditViewModel = (AdminEventEditViewModel)Session["adminEventEditViewModel"];
            adminEventEditViewModel = CreateAdminEventEditViewModel(adminEventEditViewModel.EventType);
            Session["adminEventEditViewModel"] = adminEventEditViewModel;
            return View(adminEventEditViewModel);
        }

        //Creates a new band in the database
        [Authorize]
        [HttpPost]
        public ActionResult CreateBand(AdminBand adminBand)
        {
            if (ModelState.IsValid)
            {
                var filename = CreateFileName(adminBand.Band.Name);
                var path = Path.Combine(Server.MapPath(bandImagePath), filename);
                adminBand.Band.ImagePath = bandImagePath + filename;
                Image sourceimage = Image.FromStream(adminBand.File.InputStream);
                sourceimage.Save(path, ImageFormat.Jpeg);

                bandRepository.AddBand(adminBand.Band);
            }
            else
            {
                ModelState.AddModelError("Error", "One or more Fields were empty.");
            }
            var adminEventEditViewModel = (AdminEventEditViewModel)Session["adminEventEditViewModel"];
            adminEventEditViewModel.AdminBand = adminBand;
            Session["adminEventEditViewModel"] = adminEventEditViewModel;
            return RedirectToAction("AdminEventEdit");
        }

        //Creates a new Concert in the database
        [Authorize]
        [HttpPost]
        public ActionResult CreateConcert(AdminConcert adminConcert)
        {
            adminConcert.Concert.TableType = "Concert";
            adminConcert.Concert.AvailableSeats = hallRepository.GetHall(adminConcert.Concert.HallId).Seats;
            if (ModelState.IsValid)
            {
                concertRepository.AddConcert(adminConcert.Concert);
            }
            else
            {
                ModelState.AddModelError("Error", "One or more Fields were empty.");
            }
            var adminEventEditViewModel = (AdminEventEditViewModel)Session["adminEventEditViewModel"];
            adminEventEditViewModel.AdminConcert = adminConcert;
            Session["adminEventEditViewModel"] = adminEventEditViewModel;
            return RedirectToAction("AdminEventEdit");
        }

        //Creates a new Location in the database
        [Authorize]
        [HttpPost]
        public ActionResult CreateLocation(Location location)
        {
            if (ModelState.IsValid)
            {
                locationRepository.AddLocation(location);
            }
            else
            {
                ModelState.AddModelError("Error", "One or more Fields were empty.");
            }
            var adminEventEditViewModel = (AdminEventEditViewModel)Session["adminEventEditViewModel"];
            adminEventEditViewModel.Location = location;
            Session["adminEventEditViewModel"] = adminEventEditViewModel;

            return RedirectToAction("AdminEventEdit");
        }

        //Creates a new Restaurant in the database
        [Authorize]
        [HttpPost]
        public ActionResult CreateAdminRestaurant(AdminRestaurant adminRestaurant)
        {
            if (ModelState.IsValid)
            {
                var filename = CreateFileName(adminRestaurant.Restaurant.Name);
                var path = Path.Combine(Server.MapPath(restaurantImagePath), filename);
                adminRestaurant.Restaurant.ImagePath = restaurantImagePath + filename;
                Image sourceimage = Image.FromStream(adminRestaurant.File.InputStream);
                sourceimage.Save(path, ImageFormat.Jpeg);
                restaurantRepository.AddRestaurant(adminRestaurant.Restaurant);
                //New DinnerSessions are created
                foreach (var day in dayRepository.GetAllDays())
                {
                    var startTime = adminRestaurant.StartTime;
                    for (var i = 0; i < adminRestaurant.Sessions; i++)
                    {
                        var dinnerSession = new DinnerSession
                        {
                            DayId = day.Id,
                            AvailableSeats = adminRestaurant.Restaurant.Seats,
                            RestaurantId = adminRestaurant.Restaurant.Id,
                            Duration = adminRestaurant.Duration,
                            TableType = "DinnerSessions"
                        };
                        if (i != 0)
                        {
                            startTime.AddHours((double)adminRestaurant.Duration);
                        }
                        dinnerSession.StartTime = startTime;
                        dinnerSessionRepository.AddDinnerSession(dinnerSession);
                    }
                }

                foreach (var id in adminRestaurant.FoodTypeIdList)
                {
                    RestaurantFoodType restaurantFoodType = new RestaurantFoodType()
                    {
                        FoodTypeId = id,
                        RestaurantId = restaurantRepository.GetLastRestaurant()
                    };
                    restaurantFoodTypeRepository.AddRestaurantFoodType(restaurantFoodType);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "One or more Fields were empty.");
            }



            var adminEventEditViewModel = (AdminEventEditViewModel)Session["adminEventEditViewModel"];
            adminEventEditViewModel.AdminRestaurant = adminRestaurant;
            Session["adminEventEditViewModel"] = adminEventEditViewModel;

            return RedirectToAction("AdminEventEdit");
        }

        //Creates Excel file for download
        [Authorize]
        [HttpGet]
        public void GetExcel()
        {
            var sb = new StringBuilder();
            var concertList = concertRepository.GetAllConcerts();
            var list = new List<ExcelItem>();
            foreach(var concert in concertList)
            {
                list.Add(new ExcelItem() { Name = concert.Band.Name, AvailableSeats = concert.AvailableSeats, Day = concert.Day.Name, StartTime = concert.StartTime.ToString("HH:mm") });
            }

            var restaurantList = dinnerSessionRepository.GetAllDinnerSessions();

            foreach(var dinnerSession in restaurantList)
            {
                list.Add(new ExcelItem() { Name = dinnerSession.Restaurant.Name, AvailableSeats = dinnerSession.AvailableSeats, Day = dinnerSession.Day.Name, StartTime = dinnerSession.StartTime.ToString("HH:mm") });
            }

            var grid = new System.Web.UI.WebControls.GridView
            {
                DataSource = list
            };

            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=EventOverview.xls");
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        //Create a filename for an image so it can savely be saved on the server
        private string CreateFileName(string name)
        {
            name = Regex.Replace(name, @"\s+", "") + ".jpg";
            name = name.ToLower();
            name = name.Replace("&", "and");
            return name;
        }

        //Creates a new ViewModel using the selected Type
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

        //Creates a new model for the Dinner page
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

        //Creates a list of Restaurants to be shown on the Dinner page
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

        //Creates a Concert for the Concert page
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

        //Gets Band from database by Id
        [Authorize]
        [HttpGet]
        public JsonResult GetBand(int bandId)
        {
            Band adminBand = bandRepository.GetBand(bandId);

            return Json(adminBand, JsonRequestBehavior.AllowGet);
        }

        //Gets Location from database by Id
        [Authorize]
        [HttpGet]
        public JsonResult GetLocation(int locationId)
        {
            Location location = locationRepository.GetLocation(locationId);
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        //Gets Concert from database by Id
        [Authorize]
        [HttpGet]
        public JsonResult GetConcert(int concertId)
        {
            Concert concert = concertRepository.GetConcert(concertId);
            return Json(concert, JsonRequestBehavior.AllowGet);
        }

        //Gets Restaurant from database by Id
        [Authorize]
        [HttpGet]
        public JsonResult GetAdminRestaurant(int restaurantId)
        {
            var adminRestaurant = new AdminRestaurant
            {
                Restaurant = restaurantRepository.GetRestaurant(restaurantId),
                FoodTypeIdList = new List<int>(),
                Sessions = 0
            };
            var foodTypes = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(restaurantId).ToList();
            foreach (var foodType in foodTypes)
            {
                adminRestaurant.FoodTypeIdList.Add(foodType.Id);
            }
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
        
        //Updates Band in de database
        [Authorize]
        [HttpPost]
        public void UpdateBand(int bandId, string name, string description, bool imageChanged)
        {
            Band oldBand = bandRepository.GetBand(bandId);
            if (oldBand.Name != name && !imageChanged)
            {
                System.IO.File.Move(Path.Combine(Server.MapPath(bandImagePath), CreateFileName(oldBand.Name)), Path.Combine(Server.MapPath(bandImagePath), CreateFileName(name)));
            }
            Band band = new Band()
            {
                Id = bandId,
                Name = name,
                Description = description,
                ImagePath = bandImagePath + CreateFileName(name)
            };
            bandRepository.UpdateBand(band);
        }

        //Updates Concert in the database
        [Authorize]
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

        //Updates Location in database
        [Authorize]
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

        //Update Restaurant in database, as a consequence all Dinnersessions and RestaurantFoodtypes have to be updated as well
        [Authorize]
        [HttpPost]
        public void UpdateAdminRestaurant(int restaurantId, int availableSeats, string name, int locationId, decimal price, decimal reducedPrice, int stars, string description, int sessions, string startTime, int[] foodTypeArray, decimal duration, bool imageChanged)
        {
            Restaurant oldRestaurant = restaurantRepository.GetRestaurant(restaurantId);
            if (oldRestaurant.Name != name && !imageChanged)
            {
                System.IO.File.Move(Path.Combine(Server.MapPath(bandImagePath), CreateFileName(oldRestaurant.Name)), Path.Combine(Server.MapPath(bandImagePath), CreateFileName(name)));
            }
            Restaurant restaurant = new Restaurant()
            {
                Id = restaurantId,
                Description = description,
                LocationId = locationId,
                Name = name,
                Price = price,
                Seats = availableSeats,
                Stars = stars,
                ReducedPrice = reducedPrice,
                ImagePath = restaurantImagePath + CreateFileName(name)
            };
            restaurantRepository.UpdateRestaurant(restaurant);

            UpdateDinnersSessions(duration, startTime, restaurantId, availableSeats, sessions);

            UpdateRestaurantFoodTypes(foodTypeArray, restaurantId);
        }

        //Updates the DinnerSessions
        private void UpdateDinnersSessions(decimal Duration, string StartTime, int RestaurantId, int AvailableSeats, int Sessions)
        {
            var dinnerSessions = dinnerSessionRepository.GetAllDinnerSessionsByRestaurantId(RestaurantId);
            var startDateTime = Convert.ToDateTime(StartTime);
            //Check if the number of sessions have been changed
            //If the number is not changed, update the dinner sessions
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
            //If the number of sessions has been changed recreate the Dinnersessions
            else
            {
                dinnerSessionRepository.DeleteDinnerSessions(dinnerSessions.ToList());
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

        //Updates RestaurantFoodTypes
        private void UpdateRestaurantFoodTypes(int[] foodTypeArray, int restaurantId)
        {
            //First remove all FoodTypes
            restaurantFoodTypeRepository.DeleteRestaurantFoodTypes(restaurantFoodTypeRepository.GetRestaurantFoodTypesByRestaurantId(restaurantId).ToList());

            //Create the new FoodTypes
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


        //Uploads the Image of the Band to the server
        [Authorize]
        [HttpPost]
        public void UploadBandImage()
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

        //Uploads the image of the Restaurant to the server
        [Authorize]
        [HttpPost]
        public void UploadRestaurantImage()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath(restaurantImagePath), fileName);

                Image sourceimage = Image.FromStream(file.InputStream);
                sourceimage.Save(path, ImageFormat.Jpeg);
            }
        }

        //Removes selected Band (As a consequence all Concerts and Tickets are removed as well)
        [Authorize]
        [HttpGet]
        public void RemoveBand(int bandId)
        {
            Band band = bandRepository.GetBand(bandId);
            bandRepository.RemoveBand(band);

            string fullPath = Request.MapPath(band.ImagePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        //Removes selected Concert (As a consequence all related Tickets are removed as well)
        [Authorize]
        [HttpGet]
        public void RemoveConcert(int concertId)
        {
            var concert = concertRepository.GetConcert(concertId);
            concertRepository.RemoveConcert(concert);

        }

        //Removes selected Location(As a consequence all related Concerts and or Restaurants are removed as well)
        [Authorize]
        [HttpGet]
        public void RemoveLocation(int locationId)
        {
            var location = locationRepository.GetLocation(locationId);
            locationRepository.RemoveLocation(location);
        }

        //Removes selected Restaurant (As a consequence all related Tickets are removed as well)
        [Authorize]
        [HttpGet]
        public void RemoveRestaurant(int restaurantId)
        {
            var restaurant = restaurantRepository.GetRestaurant(restaurantId);
            restaurantRepository.RemoveRestaurant(restaurant);

            string fullPath = Request.MapPath(restaurant.ImagePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
