using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class DinnerDetails
    {
        public  Restaurant restaurant { get; set; }
        public string foodtype { get; set; }
        public string duration { get; set; }
        public string startTimes { get; set; }
    }
}