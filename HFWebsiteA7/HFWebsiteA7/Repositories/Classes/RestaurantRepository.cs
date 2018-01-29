using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;
using HFWebsiteA7.Repositories.Interfaces;

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

        public void UpdateRestaurant(Restaurant restaurant)
        {
            var response = GetRestaurant(restaurant.Id);
            response.Description = restaurant.Description;
            response.ImagePath = restaurant.ImagePath;
            response.LocationId = restaurant.LocationId;
            response.Name = restaurant.Name;
            response.Price = restaurant.Price;
            response.Seats = restaurant.Seats;
            response.Stars = restaurant.Stars;

            db.SaveChanges();
        }
    }
}