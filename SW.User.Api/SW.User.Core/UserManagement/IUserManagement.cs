using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SW.User.Data.Models;

namespace SW.User.Core.UserManagement
{
    public interface IUserManagement
    {
        Task<bool> AddUserAsync(RegisterModel register, bool isAdmin);

        Task<bool> DeleteUserAsync(int Id);

        bool UpdateUser(UserInfo user);

        UserInfo GetUserById(int id);

        UserInfo GetUserByEmail(string email);

        List<UserInfo> GetAllUsers();
    }
}
