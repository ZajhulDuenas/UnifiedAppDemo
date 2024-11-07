
using Front.Infrastructure.ClientApi;
using MauiAppDemo.ViewModels;


namespace MauiAppDemo.Pages
{
    public partial class LoginPage : ContentPage
    {
        private readonly ITokenServiceClient _tokenServiceClient;

        public LoginPage()
        {
            InitializeComponent();
        }


        public LoginPage(LoginViewModel viewModel)
        {
            
            BindingContext = viewModel;

        }

        /*
        public LoginPage(ITokenServiceClient tokenServiceClient)
        {
            _tokenServiceClient = tokenServiceClient;
        }
        */

        /*
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {


            // Lógica de autenticación (ejemplo básico)
            if (UserName == "usuario" && Password == "contraseña")
            {

                // Guardar token
                Preferences.Set("nombre_usuario", "Juan");

                // Redirigir a la página principal si el login es exitoso
                Application.Current.MainPage = new AppShell(); // O AppShell si usas Shell
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas. Inténtalo de nuevo.", "OK");
            }
        }

        */
    }
}