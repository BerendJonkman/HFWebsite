using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurant(int restaurantId);
        void AddRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
    }
}
