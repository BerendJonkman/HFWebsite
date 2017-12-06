using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class HallRepository : IHallRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddHall(Hall hall)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hall> GetAllHalls()
        {
            throw new NotImplementedException();
        }

        public Hall GetHall(int hallId)
        {
            throw new NotImplementedException();
        }
    }
}