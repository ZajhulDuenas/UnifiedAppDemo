using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Front.Infrastructure.ClientApi
{
    public class JwtTokenService
    {
        public static IEnumerable<Claim> GetClaimsFromJwt(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();

            // Verificar si el token es válido y tiene el formato correcto
            if (handler.CanReadToken(jwtToken))
            {
                // Decodificar el token JWT
                var token = handler.ReadJwtToken(jwtToken);

                // Extraer los claims del token
                return token.Claims;
            }

            throw new ArgumentException("El token JWT no es válido o tiene un formato incorrecto.");
        }
    }
}
