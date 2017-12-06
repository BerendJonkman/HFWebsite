using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            throw new NotImplementedException();
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}