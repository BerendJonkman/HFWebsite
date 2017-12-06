using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEvent(int eventId);
        void AddEvent(Event myEvent);
    }
}
