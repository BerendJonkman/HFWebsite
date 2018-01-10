using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;

namespace HFWebsiteA7.Repositories.Classes
{
    public class DinnerSessionRepository : IDinnerSessionRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddDinnerSession(DinnerSession dinnerSession)
        {
            db.DinnerSessions.Add(dinnerSession);
            db.SaveChanges();
        }

        public int CountDinnerSessions(int restaurantId)
        {
            return db.DinnerSessions.GroupBy(d => d.RestaurantId == restaurantId).Count();
        }

        public IEnumerable<DinnerSession> GetAllDinnerSessions()
        {
            return db.DinnerSessions.ToList();
        }

        public DinnerSession GetDinnerSession(int dinnerSessionId)
        {
            return db.DinnerSessions.Find(dinnerSessionId);
        }

        public DinnerSession GetDinnerSessionByRestaurantId(int restaurantId)
        {
            return db.DinnerSessions.FirstOrDefault(d => d.RestaurantId == restaurantId);
        }
    }
}