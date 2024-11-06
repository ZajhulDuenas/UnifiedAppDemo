using System;
using Microsoft.Maui.Controls;

namespace MauiAppDemo.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Lógica de autenticación (ejemplo básico)
            if (username == "usuario" && password == "contraseña")
            {
                // Redirigir a la página principal si el login es exitoso
                Application.Current.MainPage = new AppShell(); // O AppShell si usas Shell
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas. Inténtalo de nuevo.", "OK");
            }
        }
    }
}