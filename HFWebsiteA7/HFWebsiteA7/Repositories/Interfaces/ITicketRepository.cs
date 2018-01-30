using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface ITicketRepository
    {
        IEnumerable<Ticket> GetAllTickets();
        Ticket GetTicket(int ticketId);
        void AddTicket(Ticket ticket);
        IEnumerable<Ticket> GetTicketsByOrderId(int orderId);
    }
}
