using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class EventRepository : IEventRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddEvent(Event myEvent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public Event GetEvent(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}