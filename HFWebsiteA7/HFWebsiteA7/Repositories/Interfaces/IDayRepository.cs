using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories
{
    interface IDayRepository
    {
        IEnumerable<Day> GetAllDays();
        Day GetDay(int dayId);
        void AddDay(Day day);
    }
}
