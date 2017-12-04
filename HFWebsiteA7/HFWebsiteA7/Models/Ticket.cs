using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Ticket
    {
        public virtual int Id { get; set; }
        public virtual int EventId { get; set; }
        public virtual Event Event { get; set; }
        public virtual string Code { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual bool Paid { get; set; }
    }
}