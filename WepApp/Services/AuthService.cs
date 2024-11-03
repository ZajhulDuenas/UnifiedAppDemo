using Microsoft.JSInterop;
public class AuthService
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _http;

    public AuthService(IJSRuntime js, HttpClient http)
    {
        _js = js;
        _http = http;
    }

    public async Task<bool> IsAuthenticated()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        return !string.IsNullOrEmpty(token);
    }

    public async Task SetToken(string token)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);
    }

    public async Task Logout()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
        // Redirige al usuario a la página de login
    }
}
