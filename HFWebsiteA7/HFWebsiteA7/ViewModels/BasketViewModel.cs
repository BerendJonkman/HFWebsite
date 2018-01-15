using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.ViewModels
{
    public class BasketViewModel
    {
        public List<Ticket> Tickets { get; set; }

        public List<PassParToutDay> Partoutdays { get; set; }

        public PassParToutWeek ParToutWeek { get; set; }
    }
}