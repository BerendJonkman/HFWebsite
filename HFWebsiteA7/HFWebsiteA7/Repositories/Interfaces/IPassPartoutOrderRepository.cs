using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IPassPartoutOrderRepository
    {
        IEnumerable<PassPartoutOrder> GetAllPassPartoutOrders();
        PassPartoutOrder GetPassPartoutOrder(int passPartoutOrderId);
        void AddPassPartoutOrder(PassPartoutOrder passPartoutOrder);
    }
}
