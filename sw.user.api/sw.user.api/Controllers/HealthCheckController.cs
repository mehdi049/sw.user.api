using Microsoft.AspNetCore.Mvc;

namespace sw.user.api.Controllers
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