using Microsoft.AspNetCore.Mvc;

namespace Kafka.API.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ProducerController : Controller
    {
        [HttpGet]
        public IActionResult Produce()
        {
            return View();
        }
    }
}
