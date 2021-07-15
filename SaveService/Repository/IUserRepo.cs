using SaveService.Auth.Api.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using SaveService.Resources.Api.Models;
using SaveService.Common.Authentication;

namespace SaveService.Auth.Api.Repository
{
    public interface IUserRepo
    {
        public Task<string> RegisterAsync(RegisterUser user, AuthOptions options);
        public Task<string> LoginAsync(LoginUser user, AuthOptions options);
    }
}
