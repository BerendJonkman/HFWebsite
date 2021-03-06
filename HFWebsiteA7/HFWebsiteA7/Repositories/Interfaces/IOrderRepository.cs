﻿using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrder(int orderId);
        void AddOrder(Order order);
        Order GetOrderByEmail(string email);
        Order GetOrderByEmailCode(string email, string code);
    }
}
