using HFWebsiteA7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class TicketRepository : ITicketRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();
        public void AddTicket(Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return db.Tickets.ToList();
        }

        public Ticket GetTicket(int ticketId)
        {
            return db.Tickets.Find(ticketId);
        }

        public IEnumerable<Ticket> GetTicketsByOrderId(int orderId)
        {
            List<Ticket> ticketList = GetAllTickets().ToList();
            return ticketList.Where(x => x.OrderId.Equals(orderId)).ToList();
        }
    }
}