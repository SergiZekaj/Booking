using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Booking.Application.Abstractions.Contracts;
using Booking.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Booking.Infrastructure.Contracts.AuthService

{
    public class AuthManager (IConfiguration _configurations) : IAuthManager
    {
        public AuthTokenResult GenerateToken(UserEntity user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var expiresAt = tokenOptions.ValidTo;
            return new AuthTokenResult(token, "Bearer", expiresAt);
        }

        private JwtSecurityToken GenerateTokenOptions (SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configurations.GetSection("JwtConfig");

            var expiration = DateTime.UtcNow.AddSeconds(
                Convert.ToDouble(jwtSettings
                .GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                
                signingCredentials: signingCredentials
            );

            return token;

        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configurations["JwtConfig:SecretKey"];
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private static List<Claim> GetClaims(UserEntity user)
        {
            var userRoles = user.UserRolesEntity
                .Select(ur => ur.Role)
                .ToList();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            claims.AddRange(userRoles
                .Select(role => new Claim(ClaimTypes.Role, role.Name)));

            return claims;
        }
    }

}
