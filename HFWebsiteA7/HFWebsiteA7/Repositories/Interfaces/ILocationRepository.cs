using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface ILocationRepository
    {
        IEnumerable<Location> GetAllLocations();
        Location GetLocation(int locationId);
        void AddLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(Location location);
    }
}
