using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IHallRepository
    {
        IEnumerable<Hall> GetAllHalls();
        Hall GetHall(int hallId);
        void AddHall(Hall hall);
    }
}
