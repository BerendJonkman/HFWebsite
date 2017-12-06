using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class LocationRepository: ILocationRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddLocation(Location location)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetAllLocations()
        {
            throw new NotImplementedException();
        }

        public Location GetLocation(int locationId)
        {
            throw new NotImplementedException();
        }
    }
}