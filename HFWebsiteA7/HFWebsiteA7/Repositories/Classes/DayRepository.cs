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
        }

        public IEnumerable<Day> GetAllDays()
        {
            throw new NotImplementedException();
        }

        public Day GetDay(int dayId)
        {
            throw new NotImplementedException();
        }
    }
}