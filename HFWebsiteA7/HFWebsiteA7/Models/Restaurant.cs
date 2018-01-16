using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Restaurant
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal ReducedPrice { get; set; }
        public virtual int Stars { get; set; }
        public virtual int Seats { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImagePath { get; set; }
        
    }
}