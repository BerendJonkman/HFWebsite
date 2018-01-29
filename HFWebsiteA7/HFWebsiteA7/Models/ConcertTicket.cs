using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class ConcertTicket : BaseTicket
    {
        public PreTicket Ticket { get; set; }
        public Concert Concert { get; set; }
        public bool Selected { get; set; }
    }
}