using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class FoodType
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}