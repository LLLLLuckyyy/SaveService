using SaveService.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SaveService.Repository
{
    public interface IUserRepo
    {
        public Task RegisterAsync(RegisterUser user, Func<string, Task> del);
        public Task LoginAsync(LoginUser user, Func<string, Task> del);
    }
}
