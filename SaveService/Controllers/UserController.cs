using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaveService.Models;
using SaveService.Repository;

namespace SaveService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _context;

        public UserController(IUserRepo context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<HttpStatusCode> Register(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.RegisterAsync(user, (login) => Authenticate(login));
                    return HttpStatusCode.OK;
                }
                catch (ArgumentException)
                {
                    return HttpStatusCode.Conflict;
                }
                catch (Exception)
                {
                    return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<HttpStatusCode> Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.LoginAsync(user, (login) => Authenticate(login));
                    return HttpStatusCode.OK;
                }
                catch (NullReferenceException)
                {
                    return HttpStatusCode.NotFound;
                }
                catch (Exception)
                {
                    return HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<HttpStatusCode> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        protected async Task Authenticate(string login)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, login));

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
