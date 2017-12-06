﻿using System;
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
            db.RestaurantFoodType.Add(restaurantFoodType);
            db.SaveChanges();
        }

        public IEnumerable<RestaurantFoodType> GetAllRestaurantFoodTypes()
        {
            return db.RestaurantFoodType.ToList();
        }

        public RestaurantFoodType GetRestaurantFoodType(int restaurantFoodTypeId)
        {
            return db.RestaurantFoodType.Find(restaurantFoodTypeId);
        }
    }
}