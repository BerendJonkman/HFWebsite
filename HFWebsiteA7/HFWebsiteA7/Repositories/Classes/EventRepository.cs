using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;

namespace HFWebsiteA7.Repositories.Classes
{
    public class EventRepository : IEventRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddEvent(Event myEvent)
        {
            db.Events.Add(myEvent);
            db.SaveChanges();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return db.Events.ToList();
        }

        public Event GetLastEvent()
        {
            return GetAllEvents().Last();
        }

        public Event GetEvent(int eventId)
        {
            return db.Events.Find(eventId);
        }

        public void UpdateEvent(Event myEvent)
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent(Event myEvent)
        {
            db.Events.Remove(myEvent);
            db.SaveChanges();
        }
    }
}