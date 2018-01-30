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
        [Required(ErrorMessage = "Street is required")]
        public int Sessions { get; set; }
        public List<FoodType> FoodTypes { get; set; }
        [Required(ErrorMessage = "Street is required")]
        public decimal Duration { get; set; }
        [Required(ErrorMessage = "At least 1 foodtype is required")]
        public List<int> FoodTypeIdList { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Required(ErrorMessage = "Street is required")]
        public DateTime StartTime { get; set; }
        public IEnumerable<SelectListItem> LocationList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeSelectList { get; set; }
        [Required(ErrorMessage = "Image is required")]
        public HttpPostedFileBase File { get; set; }
    }
}