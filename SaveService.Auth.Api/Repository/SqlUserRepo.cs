using AutoMapper;
using SaveService.Auth.Api.Authentication;
using SaveService.Auth.Api.JWT;
using SaveService.Auth.Api.Models;
using SaveService.Common.Authentication;
using SaveService.Resources.Api.Models;
using System;
using System.Threading.Tasks;

namespace SaveService.Auth.Api.Repository
{
    public class SqlUserRepo : IUserRepo
    {
        private readonly UserContext _context;
        private readonly IMapper _mapper;

        public SqlUserRepo(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> LoginAsync(LoginUser request, AuthOptions authOptions)
        {
            UserModel user = await Authenticate.AuthenticateUserAsync(request, _context);

            var token = GenerateToken.GenerateJWT(user, authOptions);

            return token;
        }

        public async Task<string> RegisterAsync(RegisterUser request, AuthOptions authOptions)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                UserModel userWithSameLogin = await Authenticate.AuthenticateUserAsync(request, _context);

                if (userWithSameLogin == null)
                {
                    UserModel newUser = _mapper.Map<UserModel>(request);
                    var token = GenerateToken.GenerateJWT(newUser, authOptions);
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return token;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
