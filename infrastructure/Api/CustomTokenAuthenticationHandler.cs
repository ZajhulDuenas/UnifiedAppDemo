using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;


namespace infrastructure.Api
{
    public class CustomTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        public CustomTokenAuthenticationHandler(
                 IOptionsMonitor<AuthenticationSchemeOptions> options,
                 ILoggerFactory logger,
                 UrlEncoder encoder,
                 ISystemClock clock)
                 : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Lee el token desde el encabezado personalizado
            if (!Request.Headers.TryGetValue("x-token", out var token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Encabezado de token no encontrado."));
            }

            // Crear identidad y claims
            var claims = new[] { new Claim(ClaimTypes.Name, "User") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

    }

 
}
