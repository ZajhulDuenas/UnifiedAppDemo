using Microsoft.AspNetCore.Components;
using WepApp.Services;
public class AuthService
{
    [Inject] protected VarsService _VarsService { get; set; }

    private string tagtoken = "authToken";
    public bool IsAuthenticated { get; private set; }

    public event Action OnAuthStateChanged;

    public AuthService(VarsService VarsService) {

        _VarsService = VarsService;
    }
    public async Task LoginAsync(string token)
    {
        IsAuthenticated = true;
        await _VarsService.SetObject<string>(tagtoken, token);
        NotifyAuthStateChanged();
    }

    /*
    public async Task<bool> IsAuthenticated()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        return !string.IsNullOrEmpty(token);
    }
    */

    public async Task<string> GetToken()
    {
        if (IsAuthenticated) {

            var token = await _VarsService.ExtractObject<string>(tagtoken);

            return token;
        }
        return "";
        
    }

    public async Task Logout()
    {
        IsAuthenticated = false;
        NotifyAuthStateChanged();
        await _VarsService.RemoveObject(tagtoken);
       
    }

    private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();

}
