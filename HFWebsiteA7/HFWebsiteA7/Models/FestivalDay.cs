using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class FestivalDay
    {
        public List<Concert> MainConcertList { get; set; }

        public List<Concert> SecondConcertList { get; set; }
    }
}