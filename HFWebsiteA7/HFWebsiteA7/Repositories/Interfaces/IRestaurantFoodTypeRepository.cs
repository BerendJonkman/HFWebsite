using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IRestaurantFoodTypeRepository
    {
        IEnumerable<RestaurantFoodType> GetAllRestaurantFoodTypes();
        RestaurantFoodType GetRestaurantFoodType(int restaurantFoodTypeId);
        void AddRestaurantFoodType(RestaurantFoodType restaurantFoodType);
    }
}
