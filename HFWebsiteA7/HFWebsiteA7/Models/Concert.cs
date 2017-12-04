using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Concert : Event
    {
        public virtual int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual int BandId { get; set; }
        public virtual Band Band { get; set; } 
        public virtual int HallId { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual decimal Duration { get; set; }
        public virtual DateTime StartTime { get; set; }
    }
}