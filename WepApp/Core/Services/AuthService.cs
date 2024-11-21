using common;
using Front.Infrastructure.ClientApi;
using Microsoft.AspNetCore.Components;
using WepApp.Core.Entities;
using WepApp.Core.Services;

public class AuthService
{
    [Inject] protected VarsService _VarsService { get; set; }
    [Inject] private IAuthServiceClient _tokenServiceClient { get; set; } = default!;

    private string tagtoken = "authToken";
    private string tagUserModel = "UserModel";

    public event Action OnAuthStateChanged;

    public AuthService(VarsService VarsService, IAuthServiceClient tokenServiceClient) 
    {

        _tokenServiceClient = tokenServiceClient;
        _VarsService = VarsService;
    }

    public async Task<bool> LoginAsync(string user, string pass)
    {
        bool result = false;

        var response = await _tokenServiceClient.OnBasicGetToken(user, pass);

        if (response.Success)
        {

            var token = response.Payload.Token;

            // extraer ckaims
            var claims_list = JwtTokenService.GetClaimsFromJwt(token);

            var LocalUser = new UserEntitie();

            LocalUser.Id = int.Parse(claims_list.FirstOrDefault(c => c.Type == "userId").Value);
            LocalUser.Name = claims_list.FirstOrDefault(c => c.Type == "realName").Value;
            LocalUser.ReadList = claims_list.FirstOrDefault(c => c.Type == "Listar").Value == "True";
            LocalUser.AddUser = claims_list.FirstOrDefault(c => c.Type == "Crear").Value == "True";
            LocalUser.ModifyUser = claims_list.FirstOrDefault(c => c.Type == "Actualizar").Value == "True";
            LocalUser.DeleteUser = claims_list.FirstOrDefault(c => c.Type == "Eliminar").Value == "True";
            LocalUser.ImportList = claims_list.FirstOrDefault(c => c.Type == "Importar").Value == "True";
            LocalUser.ExportList = claims_list.FirstOrDefault(c => c.Type == "Exportar").Value == "True";

            // save token on localstorage
            await _VarsService.SetObject<string>(tagtoken, token);
            // save user info
            await _VarsService.SetObject<UserEntitie>("UserModel", LocalUser);

            result = true;
        }

        return result;
    }

    private async Task LoginAsync(string token)
    {
       // IsAuthenticated = true;
        await _VarsService.SetObject<string>(tagtoken, token);
        NotifyAuthStateChanged();
    }

    
    public async Task<bool> CheckIsAuthenticated()
    {
        var token = await _VarsService.ExtractObject<string>(tagtoken);

        if (string.IsNullOrEmpty(token)) { return false; }

        var result = !token.IsJwtExpired();

        return result;
    }
    

    public async Task<UserEntitie> GetInfoUser()
    {
        if (await CheckIsAuthenticated()) {

            var token = await _VarsService.ExtractObject<UserEntitie>(tagUserModel);

            return token;
        }
        return null;
        
    }

    public async Task<string> GetToken()
    {
        if (await CheckIsAuthenticated())
        {

            var token = await _VarsService.ExtractObject<string>(tagtoken);

            return token;
        }
        return "";

    }

    public async Task Logout()
    {
        // IsAuthenticated = false;
        NotifyAuthStateChanged();
        await _VarsService.RemoveObject(tagtoken);
       
    }

    private void NotifyAuthStateChanged() => OnAuthStateChanged?.Invoke();

}
