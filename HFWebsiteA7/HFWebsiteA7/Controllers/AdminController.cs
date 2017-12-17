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
    public class AdminController : Controller
    {
        private IDinnerSessionRepository dinnerSessionRepository = new DinnerSessionRepository();
        private IConcertsRepository concertRepository = new ConcertRepository();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminSelection()
        {
            return View();
        }

        public ActionResult AdminEventEdit(EventTypeEnum Type)
        {
           return View(MakeAdminEventEditViewModel(Type));
        }

        public AdminEventEditViewModel MakeAdminEventEditViewModel(EventTypeEnum Type)
        {
            AdminEventEditViewModel vm = new AdminEventEditViewModel();

            if (Type.Equals(EventTypeEnum.Dinner))
            {
                vm.EventType = Type;
                vm.EventList = dinnerSessionRepository.GetAllDinnerSessions().ToList<object>();
            }else 
            if (Type.Equals(EventTypeEnum.Jazz))
            {
                vm.EventType = Type;
                vm.EventList = concertRepository.GetAllConcerts().ToList<object>();
            }
            return vm;
        }
    }
}
