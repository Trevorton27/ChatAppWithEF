using ChatAppWithEF.Data;
using ChatAppWithEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppWithEF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {

        private ChatAppDbContext _chatAppDbContext;

        public MessagesController(ChatAppDbContext chatAppDbContext)
        {
            _chatAppDbContext = chatAppDbContext;
        }

        // GET: MessagesController
        [HttpGet]
        public IActionResult GetAllMessages()
        {
            return Ok(_chatAppDbContext.Messages);
        }

        // GET: MessagesController/Details/5
        [HttpGet("{id}")]
        public IActionResult GetMessageById(int id)
        {
            var message = _chatAppDbContext.Messages.Find(id);
            if (message == null)
            {
                return NotFound("Sorry. No record for this message was found");
            }
            else
            {
                return Ok(message);
            }
        }

        //// GET: MessagesController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: MessagesController/Create
        [HttpPost]
        public IActionResult Post([FromBody] Message message)
        {
            _chatAppDbContext.Messages.Add(message);
            _chatAppDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Message message)
        {
            var updatedMessage = _chatAppDbContext.Messages.Find(id);
            if (updatedMessage == null)
            {
                return NotFound("The message you are attempting to edit does not exist.");
            }
            else
            {
             
                updatedMessage.UserId = message.UserId;
                updatedMessage.Username = message.Username;
                updatedMessage.Text = message.Text;
                _chatAppDbContext.SaveChanges();
                return Ok("This message has been successfully updated.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var message = _chatAppDbContext.Messages.Find(id);
            if (message == null)
            {
                return NotFound("The message you are attempting to delete does not exist.");
            }
            else
            {
                _chatAppDbContext.Messages.Remove(message);
                _chatAppDbContext.SaveChanges();
                return Ok("This message has been deleted.");
            }
        }
    }
}
