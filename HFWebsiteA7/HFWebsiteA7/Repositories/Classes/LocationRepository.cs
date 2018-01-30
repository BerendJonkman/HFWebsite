using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;
using System.Diagnostics;

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

        public void RemoveLocation(Location location)
        {
            db.Locations.Remove(location);
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

        public void UpdateLocation(Location location)
        {
            var result = GetLocation(location.Id);
            result.Name = location.Name;
            result.Street = location.Street;
            result.HouseNumber = location.HouseNumber;
            result.City = location.City;
            result.ZipCode = location.ZipCode;

            db.SaveChanges();
        }
    }
}