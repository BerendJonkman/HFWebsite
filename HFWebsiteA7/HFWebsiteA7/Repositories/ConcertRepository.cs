using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories
{
    public class ConcertRepository : IConcertsRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddConcert(Concert concert)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Concert> GetAllConcerts()
        {
            throw new NotImplementedException();
        }

        public Concert GetConcert(int concertId)
        {
            throw new NotImplementedException();
        }
    }
}