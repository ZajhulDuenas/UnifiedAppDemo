using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppDemo.Core.Services;
using MauiAppDemo.Pages;
using System.Windows.Input;

namespace MauiAppDemo.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {

        [ObservableProperty]
        private LoginLayout login;

        [ObservableProperty]
        private string message;

        [ObservableProperty]
        private bool isBusy;

        public ICommand LoginCommand { get; }


        public readonly AuthService _authService;

        public LoginViewModel(AuthService authService)
        {
            login = new LoginLayout();
            _authService = authService;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        // Comando para el botón de Login


        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
            {
                Message = "Campos Requeridos";

             //   await DisplayAlert("Error", "Credenciales incorrectas. Inténtalo de nuevo.", "OK");
                return;
            }
            IsBusy = true;

            var isautenticated = await _authService.LoginAsync(login.UserName, login.Password);

           
            if (isautenticated)
            {

                //  await Shell.Current.GoToAsync(nameof(HomePage));
                // Redirigir a la página principal si el login es exitoso
                Application.Current.MainPage = new AppShell(); // O AppShell si usas Shell

            }
            else
            {
                Message = "Credenciales incorrectas. Inténtalo de nuevo.";
                //  Message = "Credenciales incorrectas. Inténtalo de nuevo.";
                // Mostrar mensaje de error si la autenticación falla
            }
        }

    }

    public partial class LoginLayout : ObservableObject
    {
     
        [ObservableProperty]
        private string _userName = string.Empty;
        [ObservableProperty]
        private string _password = string.Empty;

    }
     
    


    /*
   
    public class LoginLayout : ObservableObject
    {
        
        [Required(ErrorMessage = "Usuario Requerido")]
        private string _username = string.Empty;

        [ObservableProperty]
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value); // Usamos SetProperty para que la propiedad sea notificada
        }
      
        [Required(ErrorMessage = "Password Requerido")]
        private string _password = string.Empty;

        public LoginLayout()
        {
    
        }

        [ObservableProperty]
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value); // Usamos SetProperty para que la propiedad sea notificada
        }
    }
    */

}
