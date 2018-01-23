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
        IEnumerable<PreTicket> GetAllTickets();
        PreTicket GetTicket(int ticketId);
        void AddTicket(PreTicket ticket);
    }
}
