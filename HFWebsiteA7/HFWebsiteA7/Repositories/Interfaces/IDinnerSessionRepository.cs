using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IDinnerSessionRepository
    {
        IEnumerable<DinnerSession> GetAllDinnerSessions();
        DinnerSession GetDinnerSession(int dinnerSessionId);
        void AddDinnerSession(DinnerSession dinnerSession);
        IEnumerable<DinnerSession> GetAllDinnerSessionsByRestaurantId(int restaurantId);
        DinnerSession GetDinnerSessionByRestaurantId(int restaurantId);
        int CountDinnerSessions(int restaurantId);
        void DeleteDinnerSessions(List<DinnerSession> dinnerSessions);
    }
}
