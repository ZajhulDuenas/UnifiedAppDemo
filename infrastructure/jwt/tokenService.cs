using LinqKit;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RTools_NTS.Util;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.jwt
{
    public class tokenService(IConfiguration configuration) : ITokenGenerator
    {
        private readonly IConfiguration configuration = configuration;

        public JwtSecurityToken GenerateToken(string sId, string userName,string realName, Dictionary<string, bool> dictionary, string secretKey, string issuer, string audience)
        {

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userName),
                    new Claim("realName", realName),
                    new Claim("userId", sId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                };

            foreach (var item in dictionary) {
                claims.Add(new Claim(type:item.Key, value:item.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                  
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds);

           // 
            return token;
        }

        public string GenerateStringToken(string sId, string userName, string realName, Dictionary<string, bool> dictionary, string secretKey, string issuer, string audience)
        {

            var token = this.GenerateToken(sId, userName, realName, dictionary, secretKey, issuer, audience);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
