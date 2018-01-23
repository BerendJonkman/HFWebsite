using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.ViewModels
{
    public class ReservationViewModel
    {
        public string Day { get; set; }

        public PassParToutDay PassParToutDay { get; set; }

        public PassParToutWeek PassParToutWeek { get; set; }

        public List<ConcertTicket> ConcertTickets { get; set; }
    }
}