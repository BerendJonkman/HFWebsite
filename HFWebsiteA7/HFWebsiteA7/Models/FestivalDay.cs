﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class FestivalDay
    {
        public string Day { get; set;}

        public DateTime Date { get; set; }

        public List<Concert> Concerts { get; set; }

        public string Location { get; set; }

        public List<Concert> MainConcertList { get; set; }

        public List<Concert> SecondConcertList { get; set; }
    }
}