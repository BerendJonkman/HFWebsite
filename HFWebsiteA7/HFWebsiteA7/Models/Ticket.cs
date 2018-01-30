using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Ticket
    {
        public virtual int Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int EventId { get; set; }
        public virtual string Comment { get; set; }
    }
}