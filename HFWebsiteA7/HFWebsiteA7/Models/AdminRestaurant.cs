using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HFWebsiteA7.Models
{
    public class AdminRestaurant
    {
        public Restaurant Restaurant { get; set; }
        public int Sessions { get; set; }
        public List<FoodType> FoodTypes { get; set; }
        public decimal Duration { get; set; }
        public int DayId { get; set; }
        public List<int> FoodTypeIdList { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeSelectList { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}