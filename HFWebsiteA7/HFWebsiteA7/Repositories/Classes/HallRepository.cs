using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;

namespace HFWebsiteA7.Repositories.Classes
{
    public class HallRepository : IHallRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddHall(Hall hall)
        {
            db.Halls.Add(hall);
            db.SaveChanges();
        }

        public IEnumerable<Hall> GetAllHalls()
        {
            return db.Halls.ToList();
        }

        public Hall GetHall(int hallId)
        {
            return db.Halls.Find(hallId);
        }
    }
}