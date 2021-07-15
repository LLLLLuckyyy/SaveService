using SaveService.Auth.Api.Models;
using SaveService.Resources.Api.Models;
using System.Linq;

namespace SaveService.Auth.Api.Data
{
    public static class Initialize
    {
        public static void Initializing(UserContext context)
        {
            if (!context.Users.Any())
            {
                var user = new UserModel { Login = "admin", Password = "admin" };
                context.Users.Add(user);

                context.SaveChanges();
            }
        }
    }
}
