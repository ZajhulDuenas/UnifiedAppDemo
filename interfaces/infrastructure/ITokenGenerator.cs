using System.IdentityModel.Tokens.Jwt;

namespace infrastructure.jwt
{
    public interface ITokenGenerator
    {
        JwtSecurityToken GenerateToken(string userName, Dictionary<string, bool> claimList, string secretKey, string issuer, string audience);
    }
}