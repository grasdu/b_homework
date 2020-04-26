using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.Logger.Messaging;

namespace Users.Logger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesRepository _messagesRepository;

        public MessagesController(IMessagesRepository messagesRepository)
        {
            _messagesRepository = messagesRepository ?? throw new ArgumentNullException(nameof(messagesRepository));
        }

        /// <summary>
        /// Gets a list of the Messages.
        /// </summary> 
        /// <returns>A list of Messages</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var messages = _messagesRepository.GetMessages();
            return Ok(messages);
        }

    }
}
