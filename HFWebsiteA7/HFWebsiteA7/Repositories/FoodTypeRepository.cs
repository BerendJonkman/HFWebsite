using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories
{
    public class FoodTypeRepository : IFoodTypeRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddFoodType(FoodType foodType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FoodType> GetAllFoodTypes()
        {
            throw new NotImplementedException();
        }

        public FoodType GetFoodType(int foodTypeId)
        {
            throw new NotImplementedException();
        }
    }
}