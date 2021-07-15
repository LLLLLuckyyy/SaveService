using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveService.Auth.Api.Repository
{
    //Is created to make AuthenticateUserAsync method more common for login and register models:
    //AuthenticateUserAsync(ICommonType request)
    //login and register models are implementing it
    public interface ICommonType
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
