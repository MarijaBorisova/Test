using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.Extensions.Logging;

namespace Test.Controllers
{
    [Route("authentication")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> logger;//a field of type ILogger<> used to do logging in authcontroller

        public AuthenticationController(ILogger<AuthenticationController> logger)//constructor for authcontroller
        {
            this.logger = logger;
        }

        [Route("signin")]
        public IActionResult SignIn()
        {
            logger.LogInformation($"Calling {nameof(this.SignIn)}");//logging
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });//returns a challenge result- would be used the 
            //in challenge via Facebook
            //RedirectUri responsible for redirecting the page to the appropriate page after authentication.
        }

        [Route("signout")] //action
        [HttpPost]//Post request comes with its attribute
        public async Task<IActionResult> SignOut()// acync is needed to have acync controller actions
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(".AspNetCore.ApplicationCookie");
            return RedirectToAction("Index", "Home");//because of [Authorize] in HomeContr., the visitor is taken to the login screen
        }

    }
    }
