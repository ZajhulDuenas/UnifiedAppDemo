

namespace MauiAppDemo.Pages
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar la ruta de navegación
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
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
