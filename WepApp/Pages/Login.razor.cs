using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace WepApp.Pages
{
    public partial class LoginBase : ComponentBase
    {

        [Inject] protected NavigationManager Navigation { get; set; }
        //  [Inject] protected IJSRuntime JS { get; set; } // Para interactuar con localStorage
        [Inject] protected AuthService AuthService { get; set; }
     
        protected LoginLayout LoginLayout = new LoginLayout();

        protected string Message { get; set; }

        protected async Task HandleLogin()
        {
            try
            {

                var IsAutenticated = await AuthService.LoginAsync(LoginLayout.Username, LoginLayout.Password);

                if (IsAutenticated)
                { 
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

    public class LoginLayout
    {
        [Required(ErrorMessage = "Usuario Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Requerido")]
        public string Password { get; set; }
    }

}
