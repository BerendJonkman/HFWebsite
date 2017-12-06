using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class DayRepository : IDayRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddDay(Day day)
        {
            db.Days.Add(day);
            db.SaveChanges();
        }

        public IEnumerable<Day> GetAllDays()
        {
            return db.Days.ToList();
        }

        public Day GetDay(int dayId)
        {
            return db.Days.Find(dayId);
        }
    }
}