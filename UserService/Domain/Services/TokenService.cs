using Domain.Infrastructure;
using Domain.Models;
using Domain.Services.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class TokenService : ITokenService
    {
        private IUserRegistry _UserRegistry { get; }
        private AuthenticationSettings _AuthenticationSettings { get; }

        public TokenService(IOptions<AuthenticationSettings> authenticationSettings, IUserRegistry userRegistry)
        {
            _AuthenticationSettings = authenticationSettings.Value;
            _UserRegistry = userRegistry;
        }

        public string GenerateSecurityToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_AuthenticationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddHours(_AuthenticationSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateAccountConfirmationTokenAsync(Guid userGuid)
        {
            var user = await _UserRegistry.GetAsync(userGuid);
            return await _UserRegistry.GenerateConfirmationTokenAsync(user);
        }
    }
}
