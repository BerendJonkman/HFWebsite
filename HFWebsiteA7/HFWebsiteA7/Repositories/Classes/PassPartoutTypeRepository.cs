using HFWebsiteA7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class PassPartoutTypeRepository : IPassPartoutTypeRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        public void AddPassPartoutType(PassPartoutType passPartoutType)
        {
            db.PassPartoutTypes.Add(passPartoutType);
        }

        public IEnumerable<PassPartoutType> GetAllPassPartoutType()
        {
            return db.PassPartoutTypes.ToList();
        }

        public PassPartoutType GetPassPartoutType(int passPartoutTypeId)
        {
            return db.PassPartoutTypes.Find(passPartoutTypeId);
        }
    }
}