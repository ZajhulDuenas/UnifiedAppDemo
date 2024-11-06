using MauiAppDemo.common;
using System.Text.Json;

namespace MauiAppDemo.Pages
{
    public partial class App : Application
    {
        public static AppSettings Settings { get; private set; }

        public App()
        {
            InitializeComponent();
            // Cargar configuraciones
            LoadAppSettings();


            MainPage = new LoginPage();
        }

        private void LoadAppSettings()
        {
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "appsettings.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                Settings = JsonSerializer.Deserialize<AppSettings>(json);
            }
            else
            {
                Settings = new AppSettings(); // Valores por defecto si el archivo no existe
            }
        }
    }
}
