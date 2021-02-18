using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SW.User.Api.Models;
using SW.User.Core.UserManagement;
using SW.User.Data.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SW.User.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserManagement _userManagement;

        public UserController(ILogger<UserController> logger, IUserManagement userManagement)
        {
            _logger = logger;
            _userManagement = userManagement;
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        public IActionResult GetUser(int id)
        {
            UserInfo user = _userManagement.GetUserById(id);
            if (user == null)
                return NotFound(new Response()
                {
                    Status = "Error",
                    Message = "User not found"
                });

            return Ok(user);
        }

        [HttpGet]
        [Route("getUserByEmail/{email}")]
        public IActionResult GetUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound(new Response()
                {
                    Status = "Error",
                    Message = "Email is required"
                });

            UserInfo user = _userManagement.GetUserByEmail(email);
            if (user == null)
                return NotFound(new Response()
                {
                    Status = "Error",
                    Message = "User not found"
                });

            return Ok(user);
        }


        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<UserInfo> users = _userManagement.GetAllUsers();
            if (users == null)
                return NotFound(new Response()
                {
                    Status = "Error",
                    Message = "User not found"
                });

            return Ok(users);
        }

    }
}
