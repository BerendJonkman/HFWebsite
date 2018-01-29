using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class AddConcert
    {
        [Key]
        public virtual int EventId { get; set; }
        public virtual int LocationId { get; set; }
        public virtual int BandId { get; set; }
        public virtual int HallId { get; set; }
        public virtual decimal Duration { get; set; }
        public virtual DateTime StartTime { get; set; }
    }
}