using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class PassPartoutOrder
    {
        public virtual int Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int PassPartoutId { get; set; }
    }
}