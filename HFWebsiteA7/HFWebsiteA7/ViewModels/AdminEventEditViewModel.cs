﻿using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.ViewModels
{
    public class AdminEventEditViewModel
    {
        public EventTypeEnum EventType { get; set; }
        public List<object> ObjectList { get; set; }
        public AdminBand AdminBand { get; set; }
        public Location Location { get; set; }
        public AdminConcert AdminConcert {get;set;}
        public AdminRestaurant AdminRestaurant { get; set; }
    }
}