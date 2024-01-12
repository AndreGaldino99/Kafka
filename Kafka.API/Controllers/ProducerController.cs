using Confluent.Kafka;
using Kafka.Producer.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.API.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ProducerController(IProducerService producerService) : Controller
    {
        private readonly IProducerService _producerService = producerService;

        [HttpPost("/ProducerMultiple")]
        public IActionResult ProduceMultiple(List<Message<Null, string>> messages)
        {
            _producerService.Produce(messages);
            return Ok();
        }
        
        [HttpPost]
        //public IActionResult Produce(Message<Null, string> messages)
        public IActionResult Produce(string message)
        {
            var messages = new List<Message<Null, string>>();

            messages.Add(new Message<Null, string> { Value = $"Mensagem {message}" });

            _producerService.Produce(messages);
            return Ok();
        }
    }
}
