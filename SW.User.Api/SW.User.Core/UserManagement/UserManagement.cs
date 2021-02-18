using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SW.User.Data;
using SW.User.Data.Models;

namespace SW.User.Core.UserManagement
{
    public class UserManagement : IUserManagement
    {
        private ApplicationDbContext _dbContext;

        public UserManagement(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddUser(Data.Entities.User user)
        {
            try
            {
                _dbContext.User.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public UserInfo GetUserById(int id)
        {
            Data.Entities.User user = _dbContext.User.Include(x => x.Identity).Include(y => y.Preference)
                .FirstOrDefault(x => x.Id == id);
            if (user != null)
                return new UserInfo()
                {
                    User = user,
                    ExchangesDoneCount = 10,
                    LikesCount = 25,
                    PendingExchangesCount = 8,
                    SeenCount = 112
                };

            return null;
        }

        public UserInfo GetUserByEmail(string email)
        {
            Data.Entities.User user = _dbContext.User.Include(x => x.Identity).Include(y => y.Preference).Where(x => x.Identity.Email.ToLower().Equals(email.ToLower()))
                .FirstOrDefault();
            if (user != null)
                return new UserInfo()
                {
                    User = user,
                    ExchangesDoneCount = 10,
                    LikesCount = 25,
                    PendingExchangesCount = 8,
                    SeenCount = 112
                };

            return null;
        }

        public List<UserInfo> GetAllUsers()
        {
            List<Data.Entities.User> users = _dbContext.User.Include(x => x.Identity).Include(y => y.Preference).ToList();
            if(users==null || users.Count==0)
                return null;

            List<UserInfo> usersInfo = new List<UserInfo>();
            foreach (var user in users)
                usersInfo.Add(new UserInfo()
                {
                    User = user,
                    ExchangesDoneCount = 10,
                    LikesCount = 25,
                    PendingExchangesCount = 8,
                    SeenCount = 112
                });

            return usersInfo;


        }

    }
}
