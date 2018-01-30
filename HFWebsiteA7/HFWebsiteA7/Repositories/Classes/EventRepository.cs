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

        public IEnumerable<Event> GetAllEventsForDay(int dayId)
        {
            List<Event> eventList = GetAllEvents().ToList();
            return eventList.Where(x => x.DayId.Equals(dayId)).ToList();
        }

        public void LowerAvailableSeatsforDay(int dayId, int countToLower)
        {
            foreach(Event e in GetAllEventsForDay(dayId))
            {
                if(e.TableType == "Concert")
                {
                    e.AvailableSeats -= countToLower;
                }
            }
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

        public void LowerAvailableSeats(int eventId, int countToLower)
        {
            Event eventToLower = GetEvent(eventId);
            eventToLower.AvailableSeats -= countToLower;
            db.SaveChanges();
        }

        public void LowerAllAvailableSeats(int countToLower)
        {
            foreach(Event e in GetAllEvents())
            {
                if (!e.Day.Name.Equals("Sunday"))
                {
                    e.AvailableSeats -= countToLower;
                }
            }
            db.SaveChanges();
        }
    }
}