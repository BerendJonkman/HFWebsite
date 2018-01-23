using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Band
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public virtual string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public virtual string Description { get; set; }
        public virtual string ImagePath { get; set; }
    }
}