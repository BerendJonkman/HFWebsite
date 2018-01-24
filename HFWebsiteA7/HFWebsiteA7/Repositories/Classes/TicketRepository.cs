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

        public IEnumerable<PreTicket> GetAllTickets()
        {
            return db.Tickets.ToList();
        }

        public Ticket GetTicket(int ticketId)
        {
            return db.Tickets.Find(ticketId);
        }
    }
}