using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using backend.Models;
using backend.persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{

    public class TokenPacket
    {
        public String Token { get; set; }
        public String FirstName { get; set; }
    }

    [Route("auth")]
    public class AuthController : Controller
    {

        ApiContext context;
        public AuthController(ApiContext context){
            this.context=context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginData loginData)
        {
            var user =this.context.Users.SingleOrDefault(u =>
                u.email==loginData.email &&
                u.password==loginData.password
            );

            if (user == null){
                return NotFound("password or user name is in correct");
            }
            return Ok(CreateTokenPacket(user));
        }
        [HttpPost("register")]
        public TokenPacket Register([FromBody] User user)
        {

            

           
            this.context.Users.Add(user);

            this.context.SaveChanges();

            return CreateTokenPacket(user);

            //TODO: Implement Realistic Implementation
        }

        private TokenPacket CreateTokenPacket(User user)
        {
            var signingKey = JwtSecurityKey.Create("fiversecret and more keys ");
            var signingCredentials = new SigningCredentials (signingKey,SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString())
            };
            var jwt=new JwtSecurityToken(claims:claims,signingCredentials:signingCredentials);
            
            var encodedJwt=new JwtSecurityTokenHandler().WriteToken(jwt);
            return new TokenPacket(){
                Token=encodedJwt,
                FirstName=user.firstName
            };
        }

    }

    public class LoginData
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}