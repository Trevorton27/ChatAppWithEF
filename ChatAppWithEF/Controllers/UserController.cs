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
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private ChatAppDbContext _chatAppDbContext;

        public UserController(ChatAppDbContext chatAppDbContext)
        {
            _chatAppDbContext = chatAppDbContext;
        }

        // GET: UserController
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_chatAppDbContext.Users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _chatAppDbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("No record for this user found.");
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var updatedUser = _chatAppDbContext.Users.Find(id);
            if (updatedUser == null)
            {
                return NotFound("The user you are attempting to update. Does not exist.");
            }
            else
            {
                updatedUser.Id = user.Id;
                updatedUser.Username = user.Username;
                updatedUser.FirstName = user.FirstName;
                updatedUser.LastName = user.LastName;
                updatedUser.Password = user.Password;
                updatedUser.CreatedDate = user.CreatedDate;
                _chatAppDbContext.SaveChanges();
                return Ok("User information has been successfully updated.");
            }
        }

   

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _chatAppDbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("The user you are attempting to delete does not exist.");
            }
            else
            {
                _chatAppDbContext.Users.Remove(user);
                _chatAppDbContext.SaveChanges();
                return Ok(_chatAppDbContext.Users);
            }
        }
 
    }
}
