using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class FoodTypeRepository : IFoodType
        Repository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddFoodType(FoodType foodType)
        {
            db.FoodTypes.Add(foodType);
            db.SaveChanges();
        }

        public IEnumerable<FoodType> GetAllFoodTypes()
        {
            return db.FoodTypes.ToList();
        }

        public FoodType GetFoodType(int foodTypeId)
        {
            return db.FoodTypes.Find(foodTypeId);
        }
    }
}