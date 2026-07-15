using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectHub.Application.Interfaces;
using ProjectHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHub.Persistence.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _options = jwtOptions.Value;
        }
        public string GenerateAccessToken(User user) 
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email.Value),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer:_options.Issuer,audience:_options.Audience,claims:claims,expires:DateTime.UtcNow.AddMinutes(_options.AccessTokenExpirationInMinutes),signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
