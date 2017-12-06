using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class RestaurantFoodTypeRepository : IRestaurantFoodTypeRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddRestaurantFoodType(RestaurantFoodType restaurantFoodType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RestaurantFoodType> GetAllRestaurantFoodTypes()
        {
            throw new NotImplementedException();
        }

        public RestaurantFoodType GetRestaurantFoodType(int restaurantFoodTypeId)
        {
            throw new NotImplementedException();
        }
    }
}