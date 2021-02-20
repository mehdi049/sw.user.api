using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SW.User.Api.Models;
using SW.User.Core.UserManagement;
using SW.User.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (await _userManagement.AddUserAsync(model, false))
                return Ok();

            return StatusCode(StatusCodes.Status400BadRequest, new Response { Message = "User creation failed! Please check user details and try again." });
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            if (await _userManagement.AddUserAsync(model, true))
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "User creation failed! Please check user details and try again." });
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        public IActionResult GetUser(int id)
        {
            UserInfo user = _userManagement.GetUserById(id);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Message = "User not found."
                });

            return Ok(user);
        }

        [HttpGet]
        [Route("getUserByEmail/{email}")]
        public IActionResult GetUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Message = "Email is required."
                });

            UserInfo user = _userManagement.GetUserByEmail(email);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Message = "User not found."
                });

            return Ok(user);
        }


        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<UserInfo> users = _userManagement.GetAllUsers();
            if (users == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response()
                {
                    Message = "User not found."
                });

            return Ok(users);
        }

        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            if (await _userManagement.DeleteUserAsync(id))
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError, new Response()
            {
                Message = "User deletion failed!"
            });

        }

        [HttpPost]
        [Route("updateUser")]
        public IActionResult UpdateUser([FromBody] UserInfo model)
        {
            if (_userManagement.UpdateUser(model))
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "User update failed! Please check user details and try again." });
        }

    }
}
