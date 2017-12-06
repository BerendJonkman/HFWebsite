using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class DinnerSession : Event
    {
        public virtual int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual decimal Duration { get; set; }
        public virtual DateTime StartTime { get; set; }
    }
}