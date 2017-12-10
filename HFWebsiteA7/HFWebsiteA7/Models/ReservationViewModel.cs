using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class ReservationViewModel
    {
        public string Day { get; set; }

        public List<Concert> Concerts { get; set; }
    }
}