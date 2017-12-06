using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class AdminRestaurant
    {
        public Restaurant Restaurant { get; set; }
        public int Sessions { get; set; }
        public List<FoodType> FoodTypes { get; set; }
        public decimal Duration { get; set; }
    }
}