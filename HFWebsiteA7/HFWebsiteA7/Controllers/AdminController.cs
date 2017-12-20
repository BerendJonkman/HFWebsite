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

namespace HFWebsiteA7.Controllers
{
    public class AdminController : Controller
    {
        private IDinnerSessionRepository dinnerSessionRepository = new DinnerSessionRepository();
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
                return View();
            }
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

        public AdminEventEditViewModel CreateAdminEventEditViewModel(EventTypeEnum type)
        {
            AdminEventEditViewModel vm = new AdminEventEditViewModel();
            vm.EventType = type;
            switch (type)
            {
                case EventTypeEnum.Band:
                    vm.ObjectList = bandRepository.GetAllBands().ToList<object>(); 
                    break;
                case EventTypeEnum.Concert:
                    vm.ObjectList = concertRepository.GetAllConcerts().ToList<object>();
                    break;
                case EventTypeEnum.Dinner:
                    vm.ObjectList = dinnerSessionRepository.GetAllDinnerSessions().ToList<object>();
                    break;
                case EventTypeEnum.Location:
                    vm.ObjectList = locationRepository.GetAllLocations().ToList<object>();
                    break;
            }

            return vm;
        }
    }
}
