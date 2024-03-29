﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SW.User.Data.Common;
using SW.User.Data.Models;

namespace SW.User.Core.UserManagement
{
    public interface IUserManagement
    {
        Task<Response> AddUserAsync(RegisterModel register, bool isAdmin);

        Task<Response> DeleteUserAsync(int id);

        Task<Response> UpdateUserAsync(UserInfo user);

        Response UpdateUserImage(IFormFile image, int userId, string path);

        UserInfo GetUserById(int id);

        UserInfo GetUserByEmail(string email);

        List<UserInfo> GetAllUsers();
    }
}
