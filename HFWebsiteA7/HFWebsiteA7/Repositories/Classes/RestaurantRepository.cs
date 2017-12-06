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
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            return db.Restaurants.ToList();
        }

        public Restaurant GetRestaurant(int restaurantId)
        {
            return db.Restaurants.Find(restaurantId);
        }
    }
}