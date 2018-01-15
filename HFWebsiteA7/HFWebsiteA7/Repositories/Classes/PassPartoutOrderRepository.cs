using HFWebsiteA7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class PassPartoutOrderRepository : IPassPartoutOrderRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        public void AddPassPartoutOrder(PassPartoutOrder passPartoutOrder)
        {
            db.PassPartoutOrder.Add(passPartoutOrder);
        }

        public IEnumerable<PassPartoutOrder> GetAllPassPartoutOrders()
        {
            return db.PassPartoutOrder.ToList();
        }

        public PassPartoutOrder GetPassPartoutOrder(int passPartoutOrderId)
        {
            return db.PassPartoutOrder.Find(passPartoutOrderId);
        }
    }
}