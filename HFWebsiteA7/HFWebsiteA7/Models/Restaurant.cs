using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class Restaurant
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal ReducedPrice { get; set; }
        public virtual int Stars { get; set; }
        public virtual int Seats { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual List<FoodType> FoodType { get; set; }

        public void AddFoodTypeList(IEnumerable<FoodType>foodTypeList)
        {
            //FoodType foodtype1 = new FoodType();
            //FoodType foodtype2 = new FoodType();
            //FoodType foodtype3 = new FoodType();
            //foodtype1.Id = 1;
            //foodtype1.Name = "Dutch";
            //foodtype2.Id = 2;
            //foodtype2.Name = "Greek";
            //foodtype3.Id = 3;
            //foodtype3.Name = "Turkish";

            //List<FoodType> TestList = new List<FoodType>();
            //TestList.Add(foodtype1);
            //TestList.Add(foodtype2);
            //TestList.Add(foodtype3);
            //this.FoodType = foodTypeList.ToList();
        }
    }
}