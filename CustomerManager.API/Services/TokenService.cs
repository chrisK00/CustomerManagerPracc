using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CustomerManager.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CustomerManager.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService()
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SUPER SECRET KEY"));
        }

        public string CreateToken()
        {
            var claims = new List<Claim>()
            {
              new Claim(JwtRegisteredClaimNames.NameId, "chris")
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //contents of the token
            var tokenDesc = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }
    }
}