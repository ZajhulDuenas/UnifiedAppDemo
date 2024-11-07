using Models.Services;

public interface IAuthenticationService
{
    Task<AuthResponse> BasicAuthenticateAsync(string username, string password);
}
