using HFWebsiteA7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class TicketOrderRepository : ITicketOrderRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        public void AddTicketOrder(TicketOrder ticketOrder)
        {
            db.TicketOrder.Add(ticketOrder);
            db.SaveChanges();
        }

        public IEnumerable<TicketOrder> GetAllTicketOrders()
        {
            return db.TicketOrder.ToList();
        }

        public TicketOrder GetTicketOrder(int ticketOrderId)
        {
            return db.TicketOrder.Find(ticketOrderId);
        }
    }
}