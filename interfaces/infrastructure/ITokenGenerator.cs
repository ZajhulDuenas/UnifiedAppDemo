using System.IdentityModel.Tokens.Jwt;

namespace infrastructure.jwt
{
    public interface ITokenGenerator
    {
        string GenerateStringToken(string sId, string userName,string realName, Dictionary<string, bool> dictionary, string secretKey, string issuer, string audience);
        JwtSecurityToken GenerateToken(string sId, string userName, string realName, Dictionary<string, bool> claimList, string secretKey, string issuer, string audience);
    }
}