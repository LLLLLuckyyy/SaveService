using SaveService.Resources.Api.Models;
using System.Linq;

namespace SaveService.Auth.Api.Data
{
    public static class Initialize
    {
        public static void Initializing(UserContext context)
        {
            if (!context.AppUsers.Any())
            {
                var user = new UserModel { Login = "admin", Password = "admin" };
                context.AppUsers.Add(user);

                context.SaveChanges();
            }
        }
    }
}
