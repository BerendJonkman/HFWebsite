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
            db.Locations.Add(location);
            db.SaveChanges();
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return db.Locations.ToList();
        }

        public Location GetLocation(int locationId)
        {
            return db.Locations.Find(locationId);
        }
    }
}