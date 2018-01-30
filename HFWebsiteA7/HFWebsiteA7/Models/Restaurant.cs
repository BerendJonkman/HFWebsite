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
        [Required(ErrorMessage = "Name is required")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public virtual int LocationId { get; set; }
        public virtual Location Location { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public virtual decimal Price { get; set; }
        [Required(ErrorMessage = "Price (-12) is required")]
        public virtual decimal ReducedPrice { get; set; }
        [Required(ErrorMessage = "Number of stars is required")]
        [Range(0, 5)]
        public virtual int Stars { get; set; }
        [Required(ErrorMessage = "Number of seats is required")]
        public virtual int Seats { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public virtual string Description { get; set; }
        public virtual string ImagePath { get; set; }
        
    }
}