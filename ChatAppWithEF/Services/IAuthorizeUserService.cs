using ChatAppWithEF.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppWithEF.Services
{
    public interface IAuthorizeUserService
    {
        Task<ActionResult> Register(User user, string password);
        Task<ActionResult> Login(string username, string password);

        Task<ActionResult> UserExists(string username);
        Task<ServiceResponse<string>> Register(User user);
    }
}
