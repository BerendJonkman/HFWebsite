using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class DinnerSessionRepository : IDinnerSessionRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddDinnerSession(DinnerSession dinnerSession)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DinnerSession> GetAllDinnerSessions()
        {
            throw new NotImplementedException();
        }

        public DinnerSession GetDinnerSession(int dinnerSessionId)
        {
            throw new NotImplementedException();
        }
    }
}