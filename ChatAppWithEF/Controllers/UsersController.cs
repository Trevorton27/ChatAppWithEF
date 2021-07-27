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
    public class UsersController : Controller
    {
        private ChatAppDbContext _chatAppDbContext;

        public UsersController(ChatAppDbContext chatAppDbContext)
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
                updatedUser.UserId = user.UserId;
                updatedUser.Username = user.Username;
                updatedUser.NewUsername = user.NewUsername;
                updatedUser.Password = user.Password;
                updatedUser.CreatedDate = user.CreatedDate;
                _chatAppDbContext.SaveChanges();
                return Ok("User information has been successfully updated.");
            }
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        { 
            if (_chatAppDbContext.Users.Any(u => u.Username == user.Username ))
            {
                throw new Exception("That user name already exists");
            }
            else
            {
                _chatAppDbContext.Users.Add(user);
                _chatAppDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
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
                return Ok("This user has been deleted.");
            }
        }
 
    }
}
