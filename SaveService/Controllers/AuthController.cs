using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SaveService.Auth.Api.Models;
using SaveService.Auth.Api.Repository;
using SaveService.Common.Authentication;
using SaveService.Resources.Api.Models;

namespace SaveService.Auth.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepo _context;
        private readonly IOptions<AuthOptions> _authOptions;

        public AuthController(IUserRepo context, IOptions<AuthOptions> authOptions)
        {
            _context = context;
            _authOptions = authOptions;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(/*[FromBody]*/ LoginUser request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await _context.LoginAsync(request, _authOptions.Value);
                    return Ok(new { access_token = token });
                }
                catch (Exception)
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterUser request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await _context.RegisterAsync(request, _authOptions.Value);
                    return Ok(new { access_token = token });
                }
                catch (Exception)
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
