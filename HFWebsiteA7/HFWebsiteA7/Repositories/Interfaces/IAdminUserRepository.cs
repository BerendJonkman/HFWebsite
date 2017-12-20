using HFWebsiteA7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFWebsiteA7.Repositories.Interfaces
{
    interface IAdminUserRepository
    {
        IEnumerable<AdminUser> GetAllAdminUsers();
        AdminUser GetAdminUser(int adminUserId);
        void AddAdminUser(AdminUser adminUser);
        AdminUser GetAdminUserByUser(AdminUser adminUser);
    }
}
