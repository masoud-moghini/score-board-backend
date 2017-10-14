using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using backend.Models;
using backend.persistance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
  public class EditProfileData
  {
      public string firstName { get; set; }
      public string lastName { get; set; }
  }

  [Route("user")]
  public class UserController : Controller
  {

    ApiContext context;
    public UserController(ApiContext context)
    {
      this.context=context;
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      
        var user =context.Users.SingleOrDefault(u => u.Id==id);
        if(user == null)
            return NotFound("id does not exist");
        return Ok(user); 
    }

    [HttpGet("me")]
    [Authorize]
    public ActionResult Get()
    {
      return Ok(GetSecureUser()); 
    }


    [HttpPost("me")]
    [Authorize]
    public ActionResult Post([FromBody]EditProfileData profileData)
    {
      var user = GetSecureUser();

      user.firstName=profileData.firstName??user.firstName;
      user.lastName=profileData.lastName??user.lastName;
      context.SaveChanges();
      return Ok(user);
    }

    public User GetSecureUser(){

      var id =Int32.Parse(HttpContext.User.Claims.First().Value) ;

      return context.Users.SingleOrDefault(u=>u.Id==id);
    }
  }

}