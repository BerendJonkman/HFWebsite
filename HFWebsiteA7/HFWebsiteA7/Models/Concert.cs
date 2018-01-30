using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    [Table("Concerts")]
    public class Concert : Event
    {
        [Required(ErrorMessage = "Location is required")]
        public virtual int LocationId { get; set; }
        public virtual Location Location { get; set; }
        [Required(ErrorMessage = "Band is required")]
        public virtual int BandId { get; set; }
        public virtual Band Band { get; set; }
        [Required(ErrorMessage = "Hall is required")]
        public virtual int HallId { get; set; }
        public virtual Hall Hall { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public virtual decimal Duration { get; set; }
        [Required(ErrorMessage = "Starttime is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public virtual DateTime StartTime { get; set; }
    }
}