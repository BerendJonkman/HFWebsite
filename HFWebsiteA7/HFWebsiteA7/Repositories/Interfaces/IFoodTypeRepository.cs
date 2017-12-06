using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories
{
    interface IFoodTypeRepository
    {
        IEnumerable<FoodType> GetAllFoodTypes();
        FoodType GetFoodType(int foodTypeId);
        void AddFoodType(FoodType foodType);
    }
}
