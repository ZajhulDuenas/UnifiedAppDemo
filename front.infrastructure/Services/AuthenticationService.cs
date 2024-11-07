using Models.Services;

public class AuthenticationService : IAuthenticationService
{
    public async Task<AuthResponse> BasicAuthenticateAsync(string username, string password)
    {
        AuthResponse result = new AuthResponse();

        // Implementa tu lógica de autenticación aquí
        await Task.Delay(1000); // Simula una llamada a la API o proceso

        result.isOk = true; 
        result.token = "";

        return result;
    }
}
