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
        private string imagePath = "~/Content/Content-images/JazzImages/BandImages/";

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
                var path = Path.Combine(Server.MapPath(imagePath), filename);
                adminBand.Band.ImagePath = imagePath + filename;
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
            if (ModelState.IsValid)
            {
                Event newEvent = new Event()
                {

                };
                eventRepository.AddEvent(adminConcert.Event);
                
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
            switch (type)
            {
                case EventTypeEnum.Bands:
                    vm.ObjectList = bandRepository.GetAllBands().ToList<object>();
                    vm.AdminBand = new AdminBand();
                    break;
                case EventTypeEnum.Concerts:
                    vm.ObjectList = concertRepository.GetAllConcerts().ToList<object>();
                    break;
                case EventTypeEnum.Restaurants:
                    vm.ObjectList = dinnerSessionRepository.GetAllDinnerSessions().ToList<object>();
                    var list = restaurantRepository.GetAllRestaurants();
                    var bla = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(list.First().Id);
                    break;
                case EventTypeEnum.Locations:
                    vm.Location = new Location();
                    vm.ObjectList = locationRepository.GetAllLocations().ToList<object>();
                    break;
            }

            return vm;
        }

        [HttpGet]
        public JsonResult GetBand(int bandId)
        {
            //int id = Int32.Parse(bandId);
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

        [HttpPost]
        public void UpdateBand(int bandId, string name, string description)
        {
            Band band = new Band()
            {
                Id = bandId,
                Name = name,
                Description = description,
                ImagePath = imagePath + CreateFileName(name)
        };
            bandRepository.UpdateBand(band);
        }

        [HttpPost]
        public void UpdateConcert()
        {
            Concert concert = new Concert()
            {

            };
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
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath(imagePath), fileName);

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
