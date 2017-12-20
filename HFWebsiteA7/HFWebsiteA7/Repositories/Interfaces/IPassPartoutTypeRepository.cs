using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IPassPartoutTypeRepository
    {
        IEnumerable<PassPartoutType> GetAllPassPartoutType();
        PassPartoutType GetPassPartoutType(int passPartoutTypeId);
        void AddPassPartoutType(PassPartoutType passPartoutType);
    }
}
