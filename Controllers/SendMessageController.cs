using Microsoft.AspNetCore.Mvc;

namespace ProducerForRabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly ICoreMessage _coreMessage;

        public SendMessageController(ICoreMessage coreMessage)
        {
            _coreMessage = coreMessage;
        }

        [HttpPost]
        public IActionResult Send(Message inputMessage)
        {
            if (inputMessage == null || string.IsNullOrEmpty(inputMessage.Text))
            {
                return BadRequest();
            }

            _coreMessage.SendMessage(inputMessage.Text);

            return Ok();
        }

    }
}
