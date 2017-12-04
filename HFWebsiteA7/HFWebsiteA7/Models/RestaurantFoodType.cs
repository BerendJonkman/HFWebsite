using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class RestaurantFoodType
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual int FoodTypeId { get; set; }
        public virtual FoodType FoodType { get; set; }
    }
}