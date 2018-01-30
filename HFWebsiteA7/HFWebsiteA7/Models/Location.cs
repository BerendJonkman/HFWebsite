using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Location
    {
        [Key]
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public virtual string Street { get; set; }
        [Required(ErrorMessage = "HouseNumber is required")]
        public virtual string HouseNumber { get; set; }
        [Required(ErrorMessage = "City is required")]
        public virtual string City { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]
        public virtual string ZipCode { get; set; }
    }
}