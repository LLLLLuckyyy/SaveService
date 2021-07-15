using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SaveService.Auth.Api.Models;
using SaveService.Common.Authentication;
using SaveService.Resources.Api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
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
            UserModel user = await AuthenticateUserAsync(request);

            var token = GenerateJWT(user, authOptions);

            return token;
        }

        public async Task<string> RegisterAsync(RegisterUser request, AuthOptions authOptions)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                UserModel userWithSameLogin = await AuthenticateUserAsync(request);

                if (userWithSameLogin == null)
                {
                    UserModel newUser = _mapper.Map<UserModel>(request);
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();

                    var token = GenerateJWT(newUser, authOptions);

                    return token;

                    await transaction.CommitAsync();
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

        private async Task<UserModel> AuthenticateUserAsync(ICommonType user)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);
        }

        private string GenerateJWT(UserModel user, AuthOptions authOptions)
        {
            var securityKey = authOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                authOptions.Issuer,
                authOptions.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authOptions.TokenLifetime),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }   
    }
}
