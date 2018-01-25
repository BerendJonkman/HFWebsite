using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public abstract class Event
    {
        [Key]
        public virtual int EventId { get; set; }
        public virtual int DayId { get; set; }
        public virtual Day Day { get; set; }
        public virtual int AvailableSeats { get; set; }
        [NotMapped]
        public virtual string Discriminator { get; set; }
    }
}