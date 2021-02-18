using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SW.User.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class HealthCheckController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}