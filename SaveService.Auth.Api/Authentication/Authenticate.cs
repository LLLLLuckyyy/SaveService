using Microsoft.EntityFrameworkCore;
using SaveService.Auth.Api.Repository;
using SaveService.Resources.Api.Models;
using System.Threading.Tasks;

namespace SaveService.Auth.Api.Authentication
{
    public static class Authenticate
    {
        public static async Task<UserModel> AuthenticateUserAsync(IAssociation user, UserContext context)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);
        }
    }
}
