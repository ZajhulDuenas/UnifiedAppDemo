using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace WepApp.Pages
{
    public partial class LoginBase : ComponentBase
    {
        [Inject] protected HttpClient Http { get; set; }
        [Inject] protected NavigationManager Navigation { get; set; }
        //  [Inject] protected IJSRuntime JS { get; set; } // Para interactuar con localStorage
        [Inject] protected AuthService AuthService { get; set; }

        protected string Username { get; set; }
        protected string Password { get; set; }
        protected string Message { get; set; }

        protected async Task HandleLogin()
        {
            try
            {
                var loginRequest = new { Username = Username, Password = Password };
                var response = await Http.PostAsJsonAsync("auth/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Asumimos que recibimos un token en la respuesta
                    var token = await response.Content.ReadAsStringAsync();


                    // Usa AuthService para guardar el token
                    await AuthService.SetToken(token);
                    // Guarda el token en el almacenamiento local (localStorage) del navegador
                    // await JS.InvokeVoidAsync("localStorage.setItem", "authToken", token);

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
