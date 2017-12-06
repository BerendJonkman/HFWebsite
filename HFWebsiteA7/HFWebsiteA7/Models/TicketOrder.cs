using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class TicketOrder
    {
        public virtual int Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}