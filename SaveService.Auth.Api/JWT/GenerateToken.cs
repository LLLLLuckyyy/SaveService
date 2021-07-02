using Microsoft.IdentityModel.Tokens;
using SaveService.Common.Authentication;
using SaveService.Resources.Api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SaveService.Auth.Api.JWT
{
    public static class GenerateToken
    {
        public static string GenerateJWT(UserModel user, AuthOptions authOptions)
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
