using LinqKit;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.jwt
{
    public class TokenGenerator : ITokenGenerator
    {
        public JwtSecurityToken GenerateToken(string userName, Dictionary<string, bool> claimList, string secretKey, string issuer, string audience)
        {

            var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                };

            claimList.ForEach(item => new Claim(ClaimTypes.Name, item.Key));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

            return token;
        }
    }
}
