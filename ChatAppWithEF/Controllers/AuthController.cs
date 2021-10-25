using AutoMapper;
//using AutoMapper.Configuration;
using ChatAppWithEF.Data;
using ChatAppWithEF.DTOs;
using ChatAppWithEF.Models;
using ChatAppWithEF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppWithEF.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorizeUserService _authorizeUserService;
public AuthController(IAuthorizeUserService authorizeUserService)
        {
            _authorizeUserService = authorizeUserService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] User userData)
        {
         var response = await _authorizeUserService.Register(
                      new User { Username = userData.Username, Password = userData.Password } 
                  );

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
