using Application.Common.Abstractions;
using Domain.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infrasturucture.Authentication
{
    internal sealed class JwtTokenGenrator : IJwtTokenGenarator
    {
        private readonly JwtOptions _options;
        private readonly IDateTimeProvider _dateTime;

        public JwtTokenGenrator(IOptions<JwtOptions> options,IDateTimeProvider dateTime)
                {
            _options = options.Value;
            _dateTime = dateTime;
        }
        public AccessToken GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Name,user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
           
            claims.AddRange(user.Roles.Select(r=>new Claim(CustomClaimTypes.Role,r.Name)));
            claims.AddRange(user.GetAllPermissionCodes().Select(r => new Claim(CustomClaimTypes.Permission,r)));

            var now = _dateTime.UtcNow;
            var expires = now.AddMinutes(_options.ExpirationMinutes);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var descriptor = new SecurityTokenDescriptor
            {
               
               
                    Subject = new ClaimsIdentity(claims),
                    Issuer = _options.Issuer,
                    Audience = _options.Audience,
                    IssuedAt = now,
                    NotBefore = now,
                    Expires = expires,
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                
            };

            var token = new JsonWebTokenHandler().CreateToken(descriptor);

            return new AccessToken(token,expires);
        }
    }
}
