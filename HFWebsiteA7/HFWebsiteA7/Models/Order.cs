using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual bool Paid { get; set; }
    }
}