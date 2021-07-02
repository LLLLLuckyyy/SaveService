using SaveService.Auth.Api.Models;
using System.Threading.Tasks;
using SaveService.Common.Authentication;

namespace SaveService.Auth.Api.Repository
{
    public interface IUserRepo
    {
        public Task<string> RegisterAsync(RegisterUser user, AuthOptions options);
        public Task<string> LoginAsync(LoginUser user, AuthOptions options);
    }
}
