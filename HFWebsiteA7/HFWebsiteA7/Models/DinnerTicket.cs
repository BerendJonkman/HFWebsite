using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class DinnerTicket : BaseTicket
    {
        public PreTicket Ticket { get; set; }
        public Restaurant Restaurant { get; set; }
        public string Remarks { get; set; }
    }
}