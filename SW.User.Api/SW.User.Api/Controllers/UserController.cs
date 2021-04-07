using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SW.User.Core.UserManagement;
using SW.User.Data.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SW.User.Data.Common;

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
        private readonly IWebHostEnvironment _environment;

        public UserController(ILogger<UserController> logger, IUserManagement userManagement, IWebHostEnvironment environment)
        {
            _logger = logger;
            _userManagement = userManagement;
            _environment = environment;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            Response response = await _userManagement.AddUserAsync(model, false);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterModel model)
        {
            Response response = await _userManagement.AddUserAsync(model, true);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getUserById/{id}")]
        public IActionResult GetUser(int id)
        {
            UserInfo user = _userManagement.GetUserById(id);
            if (user == null)
                return BadRequest(new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Utilisateur n'exsite pas."
                });

            return Ok(new Response { Status = HttpStatusCode.OK, Body = user });
        }

        [HttpGet]
        [Route("getUserByEmail/{email}")]
        public IActionResult GetUser(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest(new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Email est requis."
                });

            UserInfo user = _userManagement.GetUserByEmail(email);
            if (user == null)
                return BadRequest(new Response()
                {
                    Status = HttpStatusCode.BadRequest,
                    Message = "Utilisateur n'exsite pas."
                });

            return Ok(new Response { Status = HttpStatusCode.OK, Body = user });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            List<UserInfo> users = _userManagement.GetAllUsers();
            return Ok(new Response { Status = HttpStatusCode.OK, Body = users });
        }

        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            Response response = await _userManagement.DeleteUserAsync(id);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });

        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUserAsync(UserInfo model)
        {
            Response response = await _userManagement.UpdateUserAsync(model);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK });

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });
        }

        [HttpPut]
        [Route("updateUserImage/{userId}")]
        public IActionResult UpdateUserImage([FromForm] IFormFile model, int userId)
        {
            string uploadPath = _environment.WebRootPath + "\\SW\\upload\\profiles\\";

            Response response = _userManagement.UpdateUserImage(model,userId,uploadPath);
            if (response.Status == HttpStatusCode.OK)
                return Ok(new Response { Status = HttpStatusCode.OK, Body = response.Body});

            return BadRequest(new Response { Status = HttpStatusCode.BadRequest, Message = response.Message });

        }

    }
}
