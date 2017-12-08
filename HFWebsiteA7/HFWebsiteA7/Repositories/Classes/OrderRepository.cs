using HFWebsiteA7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        public void AddOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders.ToList();
        }

        public Order GetOrder(int orderId)
        {
            return db.Orders.Find(orderId);
        }
    }
}