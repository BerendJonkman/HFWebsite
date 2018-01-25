using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Event> GetAllEventsForDay(int dayId);
        Event GetEvent(int eventId);
        void AddEvent(Event myEvent);
        void LowerAvailableSeats(int eventId, int countToLower);
        void LowerAvailableSeatsforDay(int dayId, int countToLower);
        void LowerAllAvailableSeats(int countToLower);
    }
}
