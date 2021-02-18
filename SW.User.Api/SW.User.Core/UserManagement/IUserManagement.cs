using System;
using System.Collections.Generic;
using System.Text;
using SW.User.Data.Models;

namespace SW.User.Core.UserManagement
{
    public interface IUserManagement
    {
        bool AddUser(Data.Entities.User user);

        UserInfo GetUserById(int id);
    }
}
