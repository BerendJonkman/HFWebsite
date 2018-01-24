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
        public ActionResult EditBand(AdminBand adminBand)
        {
            var type = EventTypeEnum.Band;
            if (ModelState.IsValid)
            {
                

                var filename = Regex.Replace(adminBand.Band.Name, @"\s+", "") + ".jpg";
                var path = Path.Combine(Server.MapPath("~/Content/Content-images/JazzImages/BandImages/"), filename);
                adminBand.Band.ImagePath = "~/Content/Content-images/JazzImages/BandImages/" + filename;
                Image sourceimage = Image.FromStream(adminBand.File.InputStream);
                sourceimage.Save(path, ImageFormat.Jpeg);

                bandRepository.AddBand(adminBand.Band);
                var adminEventEditViewModel = CreateAdminEventEditViewModel(type);
                return View("AdminEventEdit", CreateAdminEventEditViewModel(type));
            }
            return View("AdminEventEdit", CreateAdminEventEditViewModel(type));
        }

        public AdminEventEditViewModel CreateAdminEventEditViewModel(EventTypeEnum type)
        {
            AdminEventEditViewModel vm = new AdminEventEditViewModel
            {
                EventType = type
            };
            switch (type)
            {
                case EventTypeEnum.Band:
                    vm.ObjectList = bandRepository.GetAllBands().ToList<object>();
                    vm.AdminBand = new AdminBand();
                    break;
                case EventTypeEnum.Concert:
                    vm.ObjectList = concertRepository.GetAllConcerts().ToList<object>();
                    break;
                case EventTypeEnum.Dinner:
                    vm.ObjectList = dinnerSessionRepository.GetAllDinnerSessions().ToList<object>();
                    var list = restaurantRepository.GetAllRestaurants();
                    var bla = restaurantFoodTypeRepository.GetFoodTypeByRestaurantId(list.First().Id);
                    break;
                case EventTypeEnum.Location:
                    vm.ObjectList = locationRepository.GetAllLocations().ToList<object>();
                    break;
            }

            return vm;
        }

        [HttpGet]
        public JsonResult GetBand(int bandId)
        {
            //int id = Int32.Parse(bandId);
            Band adminBand = new Band();
            foreach(Band band in bandRepository.GetAllBands())
            {
                if(band.Id == bandId)
                {
                    adminBand = band;
                }
            }
            return Json(adminBand, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void PostBand(string name, string description)
        {

        }

        [HttpPost]
        public void Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
            }
        }
    }
}
