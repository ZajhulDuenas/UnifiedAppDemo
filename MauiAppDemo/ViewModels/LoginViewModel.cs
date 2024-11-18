using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Front.Infrastructure.ClientApi;
using MauiAppDemo.Pages;
using System.Windows.Input;

namespace MauiAppDemo.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _message;

        public readonly IAuthServiceClient _tokenServiceClient;

        public ICommand LoginCommand { get; }

        public readonly IAuthenticationService _authService;

        public LoginViewModel(IAuthServiceClient tokenServiceClient, IAuthenticationService authService)
        {
            _authService = authService;
            _tokenServiceClient = tokenServiceClient;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        
       

        // Comando para el botón de Login


        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Campos Requeridos";

                // await DisplayAlert("Error", "Credenciales incorrectas. Inténtalo de nuevo.", "OK");
                return;
            }

            
           // var baseUrl = 
            
            
            var result = await _authService.BasicAuthenticateAsync(Username, Password);
            
            
            
            
            if (result.isOk)
            {

                //  await Shell.Current.GoToAsync(nameof(HomePage));
                // Redirigir a la página principal si el login es exitoso
                Application.Current.MainPage = new AppShell(); // O AppShell si usas Shell

            }
            else
            {
                Message = "Credenciales incorrectas. Inténtalo de nuevo.";
                // Mostrar mensaje de error si la autenticación falla
            }
        }

    }
}
