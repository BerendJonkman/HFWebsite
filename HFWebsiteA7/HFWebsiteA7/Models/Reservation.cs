using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Reservation
    {
        public List<object> Tickets { get; set; }
        public List<PassParToutDay> PassParToutDays { get; set; }
        public PassParToutWeek PassParToutWeek { get; set; }
    }
}