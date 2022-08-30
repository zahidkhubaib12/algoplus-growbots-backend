using GrowBotsAlgoplus.Application;
using GrowBotsAlgoplus.Extensions;
using GrowBotsAlgoplus.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GrowBotsAlgoplus.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;        
        public IAccountApplication AccountApplication { get; set; }

        public AccountController(IAccountApplication accountApplication, JwtSettings jwtSettings)
        {           
            AccountApplication = accountApplication;
            this.jwtSettings = jwtSettings;
        }

        


        //[HttpPost("GetToken")]
        //public IActionResult GetToken(Login userLogins)
        //{
        //    try
        //    {
        //        string passwordHash = BCrypt.Net.BCrypt.HashPassword(userLogins.Password);
        //        bool verified = BCrypt.Net.BCrypt.Verify(userLogins.Password, passwordHash);

        //        var Token = new UserTokens();
        //        var Valid = logins.Any(x => x.Email.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
        //        if (Valid)
        //        {
        //            var user = logins.FirstOrDefault(x => x.Email.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
        //            Token = JwtHelpers.GenTokenkey(new UserTokens()
        //            {
        //                EmailId = user.Email,                                              
        //                Id = user.Id,
        //            }, jwtSettings);
        //        }
        //        else
        //        {
        //            return BadRequest("wrong password");
        //        }
        //        return Ok(Token);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpGet("GetInfo")]
        //public string GetInfo()
        //{
        //    return "GrowBots Project for lead generation";
        //}
        
       
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login user)
        {
            var resultUser = await this.AccountApplication.GetUserByEmail(user.Email);
            if (resultUser != null)
            {                
                bool verified = BCrypt.Net.BCrypt.Verify(user.Password, resultUser.PasswordHash);
                if (verified)
                {
                    var Token = new UserTokens();
                    Token = JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        EmailId = user.Email,
                        UserId = resultUser.UserId,
                    }, jwtSettings);
                    return Ok(Token);
                }
                else
                {
                    return BadRequest("Invalid Username or Password");
                }
            }

            return BadRequest("Username does not exists. Please enter valid username");


        }


        [HttpPost("RegisterUser")]
        public async Task<int> RegisterUser([FromBody] Users user)
        {
            string pwd = "Algo@1234";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(pwd);
            user.PasswordHash = passwordHash;

            return await this.AccountApplication.RegisterUser(user);
        }

        [HttpPut("UpdateUser")]
        public async Task<bool> UpdateUser([FromBody] Users user)
        {
            return await this.AccountApplication.UpdateUser(user);
        }


        [HttpGet("GetUserByEmail")]
        public async Task<Users> GetUserByEmail([FromQuery] string email)
        {
            return await this.AccountApplication.GetUserByEmail(email);
        }

        [HttpGet("GetUserById")]
        public async Task<Users> GetUserById([FromQuery] int UserId)
        {
            return await this.AccountApplication.GetUserById(UserId);
        }

        [HttpGet("GetUsersList")]
        public async Task<List<Users>> GetUsersList()
        {
            return await this.AccountApplication.GetUsersList();
        }

        [HttpPut("ForgotPassword")]
        public async Task<bool> ForgotPassword([FromQuery] string email)
        {
            return await this.AccountApplication.ForgotPassword(email);
        }

        [HttpPut("PasswordReset")]
        public async Task<bool> PasswordReset([FromBody] Users users)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(users.PasswordHash);
            users.PasswordHash = passwordHash;
            return await this.AccountApplication.PasswordReset(users);
        }


    }
}