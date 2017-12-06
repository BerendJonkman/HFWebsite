using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Event
    {
        [Key]
        public virtual int EventId { get; set; }
        public virtual int DayId { get; set; }
        public virtual Day Day { get; set; }
        public virtual int AvailableSeats { get; set; }
        public virtual string Discriminator { get; set; }
    }
}