using ChatAppWithEF.Data;
using ChatAppWithEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppWithEF.Services
{
    public class AuthorizeUserService : IAuthorizeUserService
    {
        private readonly ChatAppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizeUserService(ChatAppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        //public async Task<ServiceResponse<string>> Login(string username, string password)
        //{
        //    ServiceResponse<string> response = new ServiceResponse<string>();
        //    User user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
        //    if (user == null)
        //    {
        //        response.Success = false;
        //        response.Message = "User not found.";
        //    }
        //    else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        // {
        //        response.Data = CreateToken(user);
        //    }
        //   {
        //        response.Success = false;
        //        response.Message = "Wrong password";
        //    }
        //    else

        //    return response;
        //}

        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (await UserExists(user.Username))
            {
                response.Success = false;
                response.Message = "This user already exists.";
                return response;
            }

            user.Password = password;
            CreateToken(user);


            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            response.Data = CreateToken(user);
            return response;

        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                //new Claim(ClaimTypes.Role, user.Role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
       Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        Task<ActionResult> IAuthorizeUserService.Register(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult> IAuthorizeUserService.UserExists(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
