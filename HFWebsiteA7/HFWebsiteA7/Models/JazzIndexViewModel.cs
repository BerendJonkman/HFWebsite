using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class JazzIndexViewModel
    {
        public List<Day> FestivalDays { get; set; }

        public List<Concert> ThursdayConcerts{ get; set; }

        public List<Concert> FridayConcerts { get; set; }

        public List<Concert> SaturdayConcerts { get; set; }

        public List<Concert> SundayConcerts { get; set; }
    }
}