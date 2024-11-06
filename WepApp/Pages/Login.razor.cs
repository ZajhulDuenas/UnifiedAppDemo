using Front.Infrastructure.ClientApi;
using Microsoft.AspNetCore.Components;
using WepApp.Models;
using WepApp.Services;

namespace WepApp.Pages
{
    public partial class LoginBase : ComponentBase
    {
        [Inject]
        private ITokenServiceClient tokenServiceClient { get; set; } = default!;
        [Inject]
        public IConfiguration configuration { get; set; } = default!;

        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }
        //  [Inject] protected IJSRuntime JS { get; set; } // Para interactuar con localStorage
        [Inject] protected AuthService AuthService { get; set; }
        [Inject] protected VarsService VarsService { get; set; }

        protected LoginModel LoginModel = new LoginModel();

       // protected string Username { get; set; }
       // protected string Password { get; set; }
        protected string Message { get; set; }

        protected async Task HandleLogin()
        {
            try
            {

                tokenServiceClient.baseEndPoint = configuration["TokenServiceSettings:UrlBase"];
                tokenServiceClient.userName = LoginModel.Username;
                tokenServiceClient.password = LoginModel.Password;

                var response = await tokenServiceClient.OnGetToken();

                if (response.Success)
                {
                    // Asumimos que recibimos un token en la respuesta
                    var token = response.Payload.Token;

                    // extraer ckaims
                    var claims_list = JwtTokenService.GetClaimsFromJwt(token);

                    var LocalUser = new UserModel();

                    LocalUser.Id = int.Parse(claims_list.FirstOrDefault(c => c.Type == "userId").Value);
                    LocalUser.Name = claims_list.FirstOrDefault(c => c.Type == "realName").Value;
                    LocalUser.ReadList = claims_list.FirstOrDefault(c => c.Type == "Listar").Value == "True";
                    LocalUser.AddUser = claims_list.FirstOrDefault(c => c.Type == "Crear").Value == "True";
                    LocalUser.ModifyUser = claims_list.FirstOrDefault(c => c.Type == "Actualizar").Value == "True";
                    LocalUser.DeleteUser = claims_list.FirstOrDefault(c => c.Type == "Eliminar").Value == "True";
                    LocalUser.ImportList = claims_list.FirstOrDefault(c => c.Type == "Importar").Value == "True";
                    LocalUser.ExportList = claims_list.FirstOrDefault(c => c.Type == "Exportar").Value == "True";

                    // Usa AuthService para guardar el token
                    await AuthService.SetToken(token);

                    // guarda info de usuario
                    await VarsService.SetObject<UserModel>("UserModel", LocalUser);

                    // Redirige al usuario a la página principal
                    Navigation.NavigateTo("/");
                }
                else
                {
                    Message = "Credenciales inválidas. Intenta de nuevo.";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error en la autenticación: {ex.Message}";
            }
        }
    }

}
