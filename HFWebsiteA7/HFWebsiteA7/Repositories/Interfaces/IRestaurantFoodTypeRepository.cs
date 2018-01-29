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
        IEnumerable<FoodType> GetFoodTypeByRestaurantId(int restaurantId);
        IEnumerable<RestaurantFoodType> GetRestaurantFoodTypesByRestaurantId(int restaurantId);
        void DeleteRestaurantFoodTypes(List<RestaurantFoodType> restaurantFoodTypes);
    }
}
