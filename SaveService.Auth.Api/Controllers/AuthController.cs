using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SaveService.Auth.Api.Models;
using SaveService.Auth.Api.Repository;
using SaveService.Common.Authentication;

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
        public async Task<ObjectResult> Login(LoginUser request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await _context.LoginAsync(request, _authOptions.Value);
                    return new ObjectResult(new { access_token = token, status_code = HttpStatusCode.OK });
                }
                catch (Exception)
                {
                    return new ObjectResult(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                return new ObjectResult(HttpStatusCode.Unauthorized);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ObjectResult> Register(RegisterUser request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var token = await _context.RegisterAsync(request, _authOptions.Value);
                    return new ObjectResult(new { access_token = token, status_code = HttpStatusCode.OK });
                }
                catch (Exception)
                {
                    return new ObjectResult(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                return new ObjectResult(HttpStatusCode.Unauthorized);
            }
        }
    }
}
