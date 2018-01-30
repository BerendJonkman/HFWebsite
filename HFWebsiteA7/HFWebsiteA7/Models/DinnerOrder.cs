using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class DinnerOrder
    {
        public Restaurant restaurant { get; set; }
        public IEnumerable<Day> days { get; set; }
        public List<DinnerSession> timeslot { get; set; }
    }
}