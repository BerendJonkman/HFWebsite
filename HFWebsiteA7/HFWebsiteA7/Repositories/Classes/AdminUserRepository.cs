using HFWebsiteA7.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HFWebsiteA7.Models;

namespace HFWebsiteA7.Repositories.Classes
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private HFWebsiteA7Context db = new HFWebsiteA7Context();

        public void AddAdminUser(AdminUser adminUser)
        {
            db.AdminUsers.Add(adminUser);
        }

        public AdminUser GetAdminUser(int adminUserId)
        {
            return db.AdminUsers.Find(adminUserId);
        }

        public AdminUser GetAdminUserByUser(AdminUser adminUser)
        {
            return db.AdminUsers.FirstOrDefault(user => user.Username == adminUser.Username && user.Password == adminUser.Password);
        }

        public IEnumerable<AdminUser> GetAllAdminUsers()
        {
            return db.AdminUsers.ToList();
        }
    }
}