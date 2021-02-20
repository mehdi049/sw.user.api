using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SW.User.Data;
using SW.User.Data.Entities;
using SW.User.Data.Models;

namespace SW.User.Core.UserManagement
{
    public class UserManagement : IUserManagement
    {
        private ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserManagement(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> AddUserAsync(RegisterModel register, bool isAdmin)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(register.Email);
                if (userExists != null)
                    return false;

                IdentityUser identity = new IdentityUser()
                {
                    Email = register.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = register.Email,
                    PhoneNumber = register.Phone
                };
                var result = await _userManager.CreateAsync(identity, register.Password);
                if (!result.Succeeded)
                    return false;

                if (!isAdmin)
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                    await _userManager.AddToRoleAsync(identity, UserRoles.User);
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    await _userManager.AddToRoleAsync(identity, UserRoles.Admin);
                }

                string identityId = identity.Id;
                Data.Entities.User user = new Data.Entities.User()
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    City = register.City,
                    Region = register.Region,
                    Gender = register.Gender,
                    Picture = register.Gender.ToLower() == "m" ? "default_m.png" : "default_f.png",
                    IdentityId = identityId,
                    Preference = new Preference()
                    {
                        DisplayPhoneNumber = false,
                        ReceiveEmail = false,
                        ReceiveNotificationNewArticle = false
                    }
                };

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
            if (users == null || users.Count == 0)
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

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                Data.Entities.User user = _dbContext.User.Include(x => x.Identity).Include(y => y.Preference).Where(x => x.Id == id).FirstOrDefault();
                _dbContext.User.Remove(user);
                _dbContext.Preference.Remove(user.Preference);
                var identity = await _userManager.FindByIdAsync(user.IdentityId);
                IdentityResult result = await _userManager.DeleteAsync(identity);
                if (!result.Succeeded)
                    return false;

                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUser(UserInfo user)
        {
            try
            {
                _dbContext.User.Update(user.User);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
