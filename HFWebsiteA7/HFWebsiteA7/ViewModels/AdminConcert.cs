using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HFWebsiteA7.ViewModels
{
    public class AdminConcert
    {
        public Concert Concert { get; set; }
        public IEnumerable<SelectListItem> DayList { get; set; }
        public IEnumerable<SelectListItem> HallList { get; set; }
        public IEnumerable<SelectListItem> BandList { get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; }
    }
}