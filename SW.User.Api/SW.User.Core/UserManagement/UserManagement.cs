using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SW.User.Data;
using SW.User.Data.Common;
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

        public async Task<Response> AddUserAsync(RegisterModel register, bool isAdmin)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(register.Email.ToLower());
                if (userExists != null)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Adresse e-mail déja existante, veuillez choisir une autre."
                    };

                IdentityUser identity = new IdentityUser()
                {
                    Email = register.Email.ToLower(),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = register.Email.ToLower(),
                    PhoneNumber = register.Phone
                };
                var result = await _userManager.CreateAsync(identity, register.Password);
                if (!result.Succeeded)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Une erreur s'est produite, veuillez réessayer."
                    };

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
                return new Response()
                {
                    Status = HttpStatusCode.OK
                };
            }
            catch
            {
                return new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public UserInfo GetUserById(int id)
        {
            Data.Entities.User user = _dbContext.User.Include(x => x.Identity).Include(y => y.Preference)
                .FirstOrDefault(x => x.Id == id);
            if (user != null)
                return new UserInfo()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    City = user.City,
                    Region = user.Region,
                    Picture = user.Picture,

                    Email = user.Identity.Email,
                    PhoneNumber = user.Identity.PhoneNumber,

                    Preference = user.Preference,

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
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    City = user.City,
                    Region = user.Region,
                    Picture = user.Picture,

                    Email = user.Identity.Email,
                    PhoneNumber = user.Identity.PhoneNumber,

                    Preference = user.Preference,

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
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    City = user.City,
                    Region = user.Region,
                    Picture = user.Picture,

                    Email = user.Identity.Email,
                    PhoneNumber = user.Identity.PhoneNumber,

                    Preference = user.Preference,

                    ExchangesDoneCount = 10,
                    LikesCount = 25,
                    PendingExchangesCount = 8,
                    SeenCount = 112
                });

            return usersInfo;
        }

        public async Task<Response> DeleteUserAsync(int id)
        {
            try
            {
                Data.Entities.User user = _dbContext.User.Include(x => x.Identity).Include(y => y.Preference).Where(x => x.Id == id).FirstOrDefault();
                if (user == null)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Utilisateur n'existe plus."
                    };

                _dbContext.User.Remove(user);
                _dbContext.Preference.Remove(user.Preference);
                var identity = await _userManager.FindByIdAsync(user.IdentityId);
                IdentityResult result = await _userManager.DeleteAsync(identity);
                if (!result.Succeeded)
                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = "Une erreur s'est produite, veuillez réessayer."
                    };

                _dbContext.SaveChanges();
                return new Response()
                {
                    Status = HttpStatusCode.OK,
                };
            }
            catch
            {
                return new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }

        public async Task<Response> UpdateUserAsync(UserInfo user)
        {
            try
            {
                var u = _dbContext.User.Include(x => x.Identity).Include(x => x.Preference)
                    .Where(x => x.Id == user.Id).FirstOrDefault();

                if (user.Email.ToLower() != u.Identity.Email.ToLower())
                {
                    var userExists = await _userManager.FindByNameAsync(user.Email);
                    if (userExists != null)
                        return new Response()
                        {
                            Status = HttpStatusCode.BadRequest,
                            Message = "Adresse e-mail déja existante, veuillez choisir une autre."
                        };
                }

                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Gender = user.Gender;
                u.City = user.City;
                u.Region = user.Region;

                u.Identity.Email = user.Email;
                u.Identity.UserName = user.Email;
                u.Identity.NormalizedUserName = user.Email.ToUpper();
                u.Identity.NormalizedEmail = user.Email.ToUpper();
                u.Identity.PhoneNumber = user.PhoneNumber;

                u.Preference = user.Preference;

                _dbContext.User.Update(u);
                _dbContext.SaveChanges();

                return new Response()
                {
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Une erreur s'est produite, veuillez réessayer."
                };
            }
        }
    }
}
