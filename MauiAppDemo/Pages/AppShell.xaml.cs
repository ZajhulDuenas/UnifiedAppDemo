
using MauiAppDemo.ViewModels;

namespace MauiAppDemo.Pages
{
    public partial class AppShell : Shell
    {
        private readonly HomeViewModel _homeViewModel;

        public AppShell()
        {
            InitializeComponent();

            // Registrar la ruta de navegación
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        }


        private async void OnHomePageSelected(object sender, EventArgs e)
        {

            var homePage = new HomePage(_homeViewModel);

            // Navegamos a la página HomePage
            await Navigation.PushAsync(homePage);
        }


        /* Cambio no fusionado mediante combinación del proyecto 'MauiAppDemo (net8.0-android)'
        Antes:
                static public void navigatePage(ContentPage contentPage) 
                {
        Después:
                static public async Task navigatePageAsync(ContentPage contentPage) 
                {
        */

    }
}
