using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;

namespace HFWebsiteA7.Repositories.Classes
{
    public class ConcertRepository : IConcertsRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddConcert(Concert concert)
        {
            db.Concerts.Add(concert);
            db.SaveChanges();
        }

        public IEnumerable<Concert> GetAllConcerts()
        {
            return db.Concerts.ToList();
        }

        public Concert GetConcert(int concertId)
        {
            return db.Concerts.Find(concertId);
        }
    }
}